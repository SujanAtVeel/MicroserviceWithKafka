using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using ProductService.Domain.Ports;
using ProductService.Infrastructure.Entities;

namespace ProductService.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(Product product, CancellationToken ct)
    {
        var productEntity = new ProductEntity
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };

        await dbContext.Products.AddAsync(productEntity, ct);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var productEntity = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        if (productEntity is null)
        {
            return null;
        }

        return Product.Rehydrate(productEntity.Id, productEntity.Name, productEntity.Price, productEntity.Quantity);
    }

    public async Task UpdateAsync(Product product, CancellationToken ct)
    {
        var productEntity = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id, ct);

        if (productEntity is null)
        {
            throw new InvalidOperationException("Product not found");
        }

        productEntity.Name = product.Name;
        productEntity.Price = product.Price;
        productEntity.Quantity = product.Quantity;

        dbContext.Products.Update(productEntity);
    }

}