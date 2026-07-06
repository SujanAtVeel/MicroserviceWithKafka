

using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Messaging;

namespace OrderService.Infrastructure.Publishers;

public class OrderEventPublisher : IOrderEventPublisher, IAsyncDisposable
{
    private readonly IProducer<string, string> producer;

    public OrderEventPublisher(IOptions<KafkaSettings> settings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = settings.Value.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = true
        };

        producer = new ProducerBuilder<string, string>(config).Build();
    }

    public Task PublishAsync(string topic, string key, string payload, CancellationToken ct)
    {
        return producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = payload }, ct);
    }

    public ValueTask DisposeAsync()
    {
        producer.Flush(TimeSpan.FromSeconds(10));
        producer.Dispose();
        return ValueTask.CompletedTask;
    }
}