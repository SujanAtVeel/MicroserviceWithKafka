using OrderService.Domain.Ports;
using OrderService.Infrastructure.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OutboxWriter : IOutboxWriter
{
    private readonly OrderDbContext dbContext;

    public OutboxWriter(OrderDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string eventType, string payload, CancellationToken ct)
    {
        await dbContext.OutboxMessages.AddAsync(new OutboxMessageEntity
        {
            EventType = eventType,
            Payload = payload
        }, ct);
    }
}