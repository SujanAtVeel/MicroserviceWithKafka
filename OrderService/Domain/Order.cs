namespace OrderService.Domain;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime OrderDate { get; private set; }

    private Order() { }

    public static Order Create(string customerName, Guid productId, int quantity)
        => new()
        {
            CustomerName = customerName,
            ProductId = productId,
            Quantity = quantity,
            OrderDate = DateTime.UtcNow
        };

    public static Order Rehydrate(Guid id, string customerName, Guid productId, int quantity, DateTime orderDate)
    {
        return new Order
        {
            Id = id,
            CustomerName = customerName,
            ProductId = productId,
            Quantity = quantity,
            OrderDate = orderDate
        };
    }
}