using Microsoft.EntityFrameworkCore;

namespace DebtMicroservice.Models
{
    public class DebtContext : DbContext
    {
        public DebtContext(DbContextOptions<DebtContext> options)
            : base(options)
        {
        }

        public DbSet<Debt> Debt { get; set; }

    }
}
