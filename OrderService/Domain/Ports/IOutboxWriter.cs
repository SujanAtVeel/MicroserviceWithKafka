namespace OrderService.Domain.Ports;

public interface IOutboxWriter
{
    Task AddAsync(string eventType, string payload, CancellationToken ct);
}