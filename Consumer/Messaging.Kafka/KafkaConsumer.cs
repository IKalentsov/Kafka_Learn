using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka;

public  class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly IMessageHandler<TMessage> _messageHandler;
    private readonly string _topic;
    private readonly IConsumer<string, TMessage> _consumer;

    public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings, IMessageHandler<TMessage> messageHandler)
    {
        _messageHandler = messageHandler;
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers,
            GroupId = kafkaSettings.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _topic = kafkaSettings.Value.Topic;

        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }

    private async Task? ConsumeAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);

                await _messageHandler.HandeAsync(result.Message.Value, stoppingToken);
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
}
