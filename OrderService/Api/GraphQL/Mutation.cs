using OrderService.Api.GraphQL.Requests;
using OrderService.Api.GraphQL.Responses;
using OrderService.Domain.Ports;

namespace OrderService.Api.GraphQL;

public class Mutation
{
    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, [Service] IOrderFacade facade, CancellationToken ct)
    {
        var newOrderId = await facade.CreateOrderAsync(request.CustomerName, request.ProductId, request.Quantity, ct);
        return new CreateOrderResponse
        {
            Id = newOrderId
        };
    }
}