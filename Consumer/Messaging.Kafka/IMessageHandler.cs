namespace Messaging.Kafka;

public interface IMessageHandler<in TMessage>
{
    Task HandeAsync(TMessage message, CancellationToken cancellationToken);
}
