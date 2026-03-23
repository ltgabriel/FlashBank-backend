using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Shared.Events;

namespace Transactions.Api.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMQ.Client.IConnection _conn;
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitMqPublisher()
        {
            var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost" };
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();

            // cola para debitar
            _channel.QueueDeclare("debitar", durable: true, exclusive: false, autoDelete: false);
            // cola para respuestas
            _channel.QueueDeclare("debito-respuestas", durable: true, exclusive: false, autoDelete: false);
        }

        public void PublicarDebitar(DebitarEvent evento)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento));
            _channel.BasicPublish("", "debitar", null, body);
        }
    }
}