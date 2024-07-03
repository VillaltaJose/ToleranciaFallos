using System.Text;
using System.Text.Json;
using Backend.API.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace server.BackgroundServices
{
    public class RabbitMQListener : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _rabbitHost;
        private readonly string _queue = "invoices";

        public RabbitMQListener(
            IServiceProvider serviceProvider,
            IConfiguration configuration
        )
        {
            _serviceProvider = serviceProvider;
            _rabbitHost = configuration[key: "RabbitMQ"]!.ToString();
            
            var factory = new ConnectionFactory() { HostName = _rabbitHost };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var request = JsonSerializer.Deserialize<Notification>(message);

                if (request is not null)
                {
                    Console.WriteLine($"=======================\nSending notification to: {request.To}\nSubject: {request.Subject}\nBody: {request.Body}\n");
                }
            };

            _channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}

