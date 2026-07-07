using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductService.Application.Events;
using ProductService.Application.Facades;

namespace ProductService.Infrastructure.Messaging;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly ILogger<OrderCreatedConsumer> logger;
    private readonly IConsumer<string, string> consumer;
    private readonly IServiceScopeFactory scopeFactory;

    public OrderCreatedConsumer(IOptions<KafkaSettings> settings, IServiceScopeFactory scopeFactory, ILogger<OrderCreatedConsumer> logger)
    {
        this.scopeFactory = scopeFactory;
        this.logger = logger;
        var config = new ConsumerConfig
        {
            BootstrapServers = settings.Value.BootstrapServers,
            GroupId = "product-service",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        consumer.Subscribe("order-created.v1");
        logger.LogInformation("OrderCreatedConsumer subscribed to orders.created.v1");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(1));
                if (result is null) continue; // no message this poll cycle, loop again

                logger.LogInformation("Consumed message at offset {Offset}", result.Offset);

                var evt = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value)!;

                using var scope = scopeFactory.CreateScope();
                var facade = scope.ServiceProvider.GetRequiredService<ProductFacade>();

                await facade.ReserveStockAsync(evt, ct);
                consumer.Commit(result);

                logger.LogInformation("Successfully processed event {EventId}", evt.EventId);
            }
            catch (ConsumeException ex)
            {
                logger.LogWarning(ex, "Kafka consume error, will retry: {Reason}", ex.Error.Reason);
                await Task.Delay(TimeSpan.FromSeconds(2), ct); // back off, then loop again instead of crashing
            }
            catch (OperationCanceledException)
            {
                break; // graceful shutdown
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error processing Kafka message");
            }
        }
    }
}
