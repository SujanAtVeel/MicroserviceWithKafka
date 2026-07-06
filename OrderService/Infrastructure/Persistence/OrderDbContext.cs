using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Entities;

namespace OrderService.Infrastructure.Persistence;

public class OrderDbContext : DbContext, IUnitOfWork
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<OutboxMessageEntity> OutboxMessages => Set<OutboxMessageEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
