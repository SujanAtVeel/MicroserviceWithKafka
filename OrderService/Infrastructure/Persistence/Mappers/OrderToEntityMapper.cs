using OrderService.Domain;
using OrderService.Infrastructure.Entities;

namespace OrderService.Infrastructure.Persistence.Mappers;

public static class OrderToEntityMapper
{
    public static OrderEntity ToEntity(Order order)
    {
        return new OrderEntity
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            OrderDate = order.OrderDate
        };
    }
}