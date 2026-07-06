using System.Text.Json;
using OrderService.Application.Events;
using OrderService.Domain;
using OrderService.Domain.Ports;

namespace OrderService.Application.Facades;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderRepository orderRepository;
    private readonly IOutboxWriter outboxWriter;
    private readonly IUnitOfWork unitOfWork;

    public OrderFacade(IOrderRepository orderRepository, IOutboxWriter outboxWriter, IUnitOfWork unitOfWork)
    {
        this.orderRepository = orderRepository;
        this.outboxWriter = outboxWriter;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateOrderAsync(string customerName, Guid productId, int quantity, CancellationToken ct)
    {
        var order = Order.Create(customerName, productId, quantity);
        await orderRepository.AddAsync(order, ct);

        var orderCreatedEvent = new OrderCreatedEvent(Guid.CreateVersion7(), order.ProductId, order.Quantity);
        await outboxWriter.AddAsync("OrderCreated", JsonSerializer.Serialize(orderCreatedEvent), ct);

        await unitOfWork.SaveChangesAsync(ct);
        return order.Id;
    }
}