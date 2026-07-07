namespace ProductService.Domain.Ports;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken ct);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(Product product, CancellationToken ct);
}