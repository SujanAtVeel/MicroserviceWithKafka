namespace OrderService.Domain.Ports;

public interface IOrderEventPublisher
{
    Task PublishAsync(string topic, string key, string payload, CancellationToken ct);
}