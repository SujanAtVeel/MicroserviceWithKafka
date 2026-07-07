using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Ports;
using static ProductService.Infrastructure.Persistence.ProductDbContext;

namespace ProductService.Infrastructure.Persistence;

public class ProcessedEventStore : IProcessedEventStore
{
    private readonly ProductDbContext dbContext;

    public ProcessedEventStore(ProductDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> IsProcessedAsync(Guid eventId, CancellationToken ct)
    {
        return await dbContext.ProcessedEvents.AnyAsync(e => e.EventId == eventId, ct);
    }

    public async Task MarkProcessedAsync(Guid eventId, CancellationToken ct)
    {
        await dbContext.ProcessedEvents.AddAsync(new ProcessedEvent { EventId = eventId }, ct);
    }
}