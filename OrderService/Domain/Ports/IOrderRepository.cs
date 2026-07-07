namespace OrderService.Domain.Ports;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken ct);

    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken ct);
}