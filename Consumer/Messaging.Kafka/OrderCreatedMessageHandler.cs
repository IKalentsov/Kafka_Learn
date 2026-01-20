using Messaging.Kafka.Models;
using Microsoft.Extensions.Logging;

namespace Messaging.Kafka;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger) : IMessageHandler<OrderCreated>
{
    public Task HandeAsync(OrderCreated message, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Order created. Start handle: {message.Id}");

        return Task.CompletedTask;
    }
}
