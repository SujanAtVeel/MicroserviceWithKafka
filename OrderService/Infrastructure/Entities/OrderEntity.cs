namespace OrderService.Infrastructure.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
}