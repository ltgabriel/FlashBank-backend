using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models;

namespace Transactions.Api.Data
{
    // db context para escritura (guarda estado de transacciones)
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}