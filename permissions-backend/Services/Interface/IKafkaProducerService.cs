namespace permissions_backend.Services.Interface;

/**
 * IKafkaProducerService interface
 */
public interface IKafkaProducerService
{
    Task SendMessageAsync(string operationName);
}