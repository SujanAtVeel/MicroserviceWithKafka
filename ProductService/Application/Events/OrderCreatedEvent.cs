namespace ProductService.Application.Events;

public record OrderCreatedEvent(Guid EventId, Guid ProductId, int Quantity);