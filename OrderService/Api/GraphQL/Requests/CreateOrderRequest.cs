namespace OrderService.Api.GraphQL.Requests;

public class CreateOrderRequest
{
    public string CustomerName { get; private set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
}