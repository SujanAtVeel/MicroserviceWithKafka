namespace ProductService.Domain.Ports;

public interface IProductFacade
{
    Task<Guid> CreateProductAsync(string name, decimal price, int quantity, CancellationToken ct);
    Task<Product?> GetProductAsync(Guid id, CancellationToken ct);

}