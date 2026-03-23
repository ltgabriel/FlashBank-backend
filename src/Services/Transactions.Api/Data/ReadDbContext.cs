using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models;

namespace Transactions.Api.Data
{
    // db context para la base de lect
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

        public DbSet<TransactionReadModel> TransactionsRead { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionReadModel>()
                .HasIndex(t => new { t.AccountId, t.CreatedAt })
                .HasDatabaseName("IX_AccountId_CreatedAt");
        }
    }
}