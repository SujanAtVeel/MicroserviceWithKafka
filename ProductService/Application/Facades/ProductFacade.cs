using ProductService.Domain;
using ProductService.Domain.Ports;
using ProductService.Application.Events;

namespace ProductService.Application.Facades;

public class ProductFacade : IProductFacade
{
    private readonly IProductRepository products;
    private readonly IProcessedEventStore processedEvents;
    private readonly IUnitOfWork unitOfWork;

    public ProductFacade(IProductRepository products, IProcessedEventStore processedEvents, IUnitOfWork unitOfWork)
    {
        this.products = products;
        this.processedEvents = processedEvents;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateProductAsync(string name, decimal price, int quantity, CancellationToken ct)
    {
        var product = Product.Create(name, price, quantity);
        await products.AddAsync(product, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return product.Id;
    }

    public async Task<Product?> GetProductAsync(Guid id, CancellationToken ct)
        => await products.GetByIdAsync(id, ct);

    public async Task ReserveStockAsync(OrderCreatedEvent evt, CancellationToken ct)
    {
        if (await processedEvents.IsProcessedAsync(evt.EventId, ct)) return;

        var product = await products.GetByIdAsync(evt.ProductId, ct)
            ?? throw new InvalidOperationException("Product not found");

        product.DecreaseQuantity(evt.Quantity);

        await products.UpdateAsync(product, ct);

        await processedEvents.MarkProcessedAsync(evt.EventId, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}