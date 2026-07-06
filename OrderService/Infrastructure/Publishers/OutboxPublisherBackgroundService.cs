using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Publishers;

public class OutboxPublisherBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly OrderEventPublisher publisher;

    public OutboxPublisherBackgroundService(IServiceScopeFactory scopeFactory, OrderEventPublisher publisher)
    {
        this.scopeFactory = scopeFactory;
        this.publisher = publisher;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

            var pending = await db.OutboxMessages
                .Where(m => m.PublishedAt == null)
                .OrderBy(m => m.CreatedAt)
                .Take(20)
                .ToListAsync(ct);

            foreach (var msg in pending)
            {
                var topic = msg.EventType switch
                {
                    "OrderCreated" => "order-created.v1",
                    _ => throw new InvalidOperationException($"Unknown event type: {msg.EventType}")
                };
                await publisher.PublishAsync(topic, msg.Id.ToString(), msg.Payload, ct);
                msg.PublishedAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync(ct);
            await Task.Delay(TimeSpan.FromSeconds(2), ct);
        }
    }
}