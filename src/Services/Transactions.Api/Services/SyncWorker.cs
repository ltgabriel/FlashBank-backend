using Transactions.Api.Data;
using Transactions.Api.Models;

namespace Transactions.Api.Services
{
    // worker que sincroniza datos cada 1-2 segundos
    public class SyncWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SyncWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var readDb = scope.ServiceProvider.GetRequiredService<ReadDbContext>();

                    Console.WriteLine("sincronizando datos a base de lectura...");

                    // simular datos de prueba si no hay
                    if (!readDb.TransactionsRead.Any())
                    {
                        var tx = new TransactionReadModel
                        {
                            Id = Guid.NewGuid(),
                            AccountId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Amount = 500,
                            Type = "debit",
                            Status = "completed",
                            CreatedAt = DateTime.Now,
                            Description = "transferencia de prueba"
                        };
                        readDb.TransactionsRead.Add(tx);
                        await readDb.SaveChangesAsync();
                        Console.WriteLine("datos de prueba insertados");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error sincronizando: {ex.Message}");
                }

                // esperar 1-2 segundos
                await Task.Delay(1500, stoppingToken);
            }
        }
    }
}