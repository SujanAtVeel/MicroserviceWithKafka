using OrderService.Domain;
using OrderService.Infrastructure.Entities;

namespace OrderService.Infrastructure.Persistence.Mappers;

public static class OrderToDomainMapper
{
    public static Order ToDomain(OrderEntity entity)
    {
        return Order.Rehydrate(entity.Id, entity.CustomerName, entity.ProductId, entity.Quantity, entity.OrderDate);
    }
}