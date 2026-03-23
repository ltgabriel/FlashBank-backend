using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Shared.Events;

namespace Accounts.Api.Services
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly RabbitMQ.Client.IConnection _conn;
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitMqConsumer()
        {
         var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost" };
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();

            _channel.QueueDeclare("debitar", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("debito-respuestas", durable: true, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
        var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
             var body = ea.Body.ToArray();
              var msg = Encoding.UTF8.GetString(body);
                var evento = JsonSerializer.Deserialize<DebitarEvent>(msg);

                if (evento != null)
                {
                    Console.WriteLine($"recibido debito para cuenta {evento.CuentaId} por {evento.Monto}");

                    // simular validacion de saldo
                    if (evento.Monto > 1000)
                    {
                        Console.WriteLine($"fallo: saldo insuficiente");
                        var fallo = new DebitoFallidoEvent
                        {
                            TransaccionId = evento.TransaccionId,
                            CuentaId = evento.CuentaId,
                            Motivo = "saldo insuficiente"
                        };
                        PublicarRespuesta(fallo);
                    }
                    else
                    {
                        Console.WriteLine($"debito exitoso");
                        var ok = new DebitoCompletadoEvent
                        {
                            TransaccionId = evento.TransaccionId,
                            CuentaId = evento.CuentaId,
                            Monto = evento.Monto
                        };
                        PublicarRespuesta(ok);
                    }
                }
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("debitar", false, consumer);
            return Task.CompletedTask;
        }
        private void PublicarRespuesta(object evento)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento));
            _channel.BasicPublish("", "debito-respuestas", null, body);
        }
    }
}