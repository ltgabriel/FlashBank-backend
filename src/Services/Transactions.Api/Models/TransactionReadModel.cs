namespace Transactions.Api.Models
{
    // tabla desnormalizada para lecturas rapidas
    public class TransactionReadModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Type { get; set; } // credito o debito
        public string? Status { get; set; } // estados pedientte completado o fallado
        public DateTime CreatedAt { get; set; }
        public string?  Description { get; set; }
    }
}