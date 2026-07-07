namespace ProductService.Domain;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private Product() { }

    public static Product Create(string name, decimal price, int quantity)
    {
        return new()
        {
            Name = name,
            Price = price,
            Quantity = quantity
        };
    }

    public static Product Rehydrate( Guid id, string name, decimal price, int quantity)
    {
        return new Product
        {
            Id = id,
            Name = name,
            Price = price,
            Quantity = quantity
        };
    }

    public void DecreaseQuantity(int amount)
    {
        if (amount > Quantity)
            throw new InvalidOperationException("Insufficient stock");
        Quantity -= amount;
    }
}