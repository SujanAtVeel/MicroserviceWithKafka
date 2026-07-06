// OrderService.Application/Events/OrderCreatedEvent.cs
namespace OrderService.Application.Events;

public record OrderCreatedEvent(Guid EventId, Guid ProductId, int Quantity);