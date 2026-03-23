namespace Transactions.Api.Models
{
    // modelo para guardar estado de transacciones
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; } // staus pedientte completado o fallado
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}