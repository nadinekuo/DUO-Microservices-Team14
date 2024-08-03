using Microsoft.EntityFrameworkCore;

namespace IdentityMicroservice.Models
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<Identity> Identity { get; set; }

    }
}
