using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Ports;
using ProductService.Infrastructure.Entities;

namespace ProductService.Infrastructure.Persistence;

public class ProductDbContext : DbContext, IUnitOfWork
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<ProcessedEvent> ProcessedEvents => Set<ProcessedEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        modelBuilder.Entity<ProcessedEvent>().HasKey(e => e.EventId);
    }

    public class ProcessedEvent
    {
        public Guid EventId { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}
