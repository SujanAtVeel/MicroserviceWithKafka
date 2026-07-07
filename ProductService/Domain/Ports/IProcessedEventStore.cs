namespace ProductService.Domain.Ports;

public interface IProcessedEventStore
{
    Task<bool> IsProcessedAsync(Guid eventId, CancellationToken ct);
    Task MarkProcessedAsync(Guid eventId, CancellationToken ct);
}