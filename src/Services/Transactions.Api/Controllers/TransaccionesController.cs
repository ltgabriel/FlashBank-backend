using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Transactions.Api.Data;
using Transactions.Api.Services;

namespace Transactions.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly RabbitMqPublisher _publisher;
        private readonly ReadDbContext _readDb;

        public TransaccionesController(ReadDbContext readDb)
        {
            _publisher = new RabbitMqPublisher();
            _readDb = readDb;
        }

        // crear transferencia (escritura)
        [HttpPost]
        public IActionResult CrearTransferencia([FromBody] CrearTransferenciaRequest req)
        {
            var transaccionId = Guid.NewGuid();

            // guarda como pendiente (simulado)
            Console.WriteLine($"transaccion {transaccionId} guardada como pendiente");

            // publica evento debitar
            var evento = new DebitarEvent
            {
                TransaccionId = transaccionId,
                CuentaId = req.CuentaId,
                Monto = req.Monto,
                Fecha = DateTime.Now
            };

            _publisher.PublicarDebitar(evento);

            return Ok(new
            {
                transaccionId,
                estado = "pendiente",
                mensaje = "evento debitar publicado"
            });
        }

        // obtener historial (lectura) - usa base de lectura
        [HttpGet("history/{accountId}")]
        public async Task<IActionResult> GetHistory(Guid accountId)
        {
            // consulta a la base de lectura, no a la tabla gigante
            var transacciones = await _readDb.TransactionsRead
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.CreatedAt)
                .Take(50)
                .ToListAsync();

            return Ok(transacciones);
        }
    }

    public class CrearTransferenciaRequest
    {
        public Guid CuentaId { get; set; }
        public decimal Monto { get; set; }
    }
}