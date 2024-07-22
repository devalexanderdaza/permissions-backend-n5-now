using Confluent.Kafka;
using System.Text.Json;
using permissions_backend.Services.Interface;

namespace permissions_backend.Services;

public class KafkaProducerService: IKafkaProducerService
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic = "permissions-topic";

    public KafkaProducerService()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendMessageAsync(string operationName)
    {
        var message = new
        {
            Id = Guid.NewGuid().ToString(),
            NameOperation = operationName
        };

        var jsonMessage = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(_topic, new Message<string, string> { Key = message.Id, Value = jsonMessage });
    }
}