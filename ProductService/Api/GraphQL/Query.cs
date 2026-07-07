using ProductService.Api.GraphQL.Responses;
using ProductService.Domain.Ports;

namespace ProductService.Api.GraphQL;

public class Query
{
    public async Task<GetProductResponse?> GetProduct(
        Guid id,
        [Service] IProductFacade facade,
        CancellationToken ct)
    {
        var product = await facade.GetProductAsync(id, ct);
        return new GetProductResponse()
        {
            Id = product!.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
}