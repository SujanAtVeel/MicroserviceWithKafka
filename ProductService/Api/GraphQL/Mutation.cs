using ProductService.Api.GraphQL.Requests;
using ProductService.Api.GraphQL.Responses;
using ProductService.Domain.Ports;

namespace ProductService.Api.GraphQL;

public class Mutation
{
   public async Task<CreateProductResponse> CreateProduct(
        CreateProductRequest input,
        [Service] IProductFacade facade,
        CancellationToken ct)
    {
        var id = await facade.CreateProductAsync(input.Name, input.Price, input.Quantity, ct);
        return new CreateProductResponse
        {
            Id = id
        };
    }
}