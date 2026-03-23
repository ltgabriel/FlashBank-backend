namespace Transactions.Api.Models
{
    // tabla outbox para eventos que no se pierdan
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string? Type { get; set; } // nombre del evento
        public string? Data { get; set; } // json del evento
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}