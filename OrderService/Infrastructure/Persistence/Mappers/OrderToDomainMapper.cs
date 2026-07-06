using OrderService.Domain;
using OrderService.Infrastructure.Entities;

namespace OrderService.Infrastructure.Persistence.Mappers;

public static class OrderToDomainMapper
{
    public static Order ToDomain(OrderEntity entity)
    {
        return Order.Create(entity.CustomerName, entity.ProductId, entity.Quantity);
    }
}