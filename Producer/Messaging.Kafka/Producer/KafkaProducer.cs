using Confluent.Kafka;
using Messaging.Kafka.Common;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka.Producer;

public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
{
    private readonly IProducer<string, TMessage> _producerBuilder;
    private readonly string _topic;

    public KafkaProducer(IOptions<KafkaSettings> kafkaSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };

        _producerBuilder = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>())
            .Build();

        _topic = kafkaSettings.Value.Topic;
    }

    public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        await _producerBuilder.ProduceAsync(_topic, new Message<string, TMessage>
        {
            Key = "uniq1",
            Value = message
        }, cancellationToken);
    }

    public void Dispose()
    {
        _producerBuilder?.Dispose();
    }
}
