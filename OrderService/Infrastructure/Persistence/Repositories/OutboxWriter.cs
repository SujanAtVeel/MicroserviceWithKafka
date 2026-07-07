using OrderService.Domain.Ports;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OutboxWriter : IOutboxWriter
{
    public Task AddAsync(string eventType, string payload, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}