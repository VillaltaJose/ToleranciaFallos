using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Backend.API.Services
{
    public class RabbitMQService
	{
        private readonly ConnectionFactory _factory;

        public RabbitMQService(string hostName)
        {
            _factory = new ConnectionFactory() { HostName = hostName };
        }

        public void SendMessage(object messageObj, string queue)
        {
            var message = JsonSerializer.Serialize(messageObj);

            SendMessage(message, queue);
        }

        public void SendMessage(string message, string queue)
        {
            try
            {
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
            }
            catch
            {

            }
        }
    }
}

