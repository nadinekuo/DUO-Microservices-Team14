using Microsoft.EntityFrameworkCore;

namespace StudentProductMicroservice.Models
{
    public class StudentProductContext : DbContext
    {
        public StudentProductContext(DbContextOptions<StudentProductContext> options)
            : base(options)
        {
        }

        public DbSet<StudentProduct> StudentProducts { get; set; }
        public DbSet<Grant> Grants { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<BasicGrant> BasicGrants {get; set;}
        public DbSet<SupplementaryGrant> SupplementaryGrants {get; set;}
        public DbSet<TravelProduct> TravelProducts {get; set;}
        public DbSet<InterestBearingLoan> InterestBearingLoans { get; set;}
        public DbSet<TuitionFeesLoan> TuitionFeesLoan { get; set;}


    }
}
