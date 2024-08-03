using Microsoft.EntityFrameworkCore;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repository
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }

        public DbSet<DebtRepayment> DebtRepayments { get; set; }

        public DbSet<Payout> Payouts { get; set; }
    }
}
