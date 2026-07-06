namespace OrderService.Domain.Ports;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken ct);
}