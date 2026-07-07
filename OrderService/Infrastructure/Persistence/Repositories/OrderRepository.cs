using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Persistence.Mappers;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Order order, CancellationToken ct)
    {
        await _dbContext.Orders.AddAsync(OrderToEntityMapper.ToEntity(order), ct);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken ct)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, ct);
        if (order == null)
        {
            return null;
        }
        return OrderToDomainMapper.ToDomain(order);
    }
}