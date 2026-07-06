namespace OrderService.Domain.Ports;

public interface IOrderFacade
{
    Task<Guid> CreateOrderAsync(string customerName, Guid productId, int quantity, CancellationToken ct);
}