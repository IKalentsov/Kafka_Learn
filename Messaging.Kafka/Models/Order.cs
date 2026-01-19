namespace Messaging.Kafka.Models;

public record Order
{
    public string Id { get; set; }
    public string Name { get; set; }
}
