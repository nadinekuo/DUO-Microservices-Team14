using Microsoft.EntityFrameworkCore;
using IdentityMicroservice.Models;
using IdentityMicroservice.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityMicroservice.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityContext _context;

        public IdentityRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Identity> GetByIdAsync(int id)
        {
            return await _context.Identity.FindAsync(id);
        }

        public async Task<IEnumerable<Identity>> GetAllAsync() 
        {
            return await _context.Identity.ToListAsync();
        }

        public async Task AddAsync(Identity identity)
        {
            await _context.Identity.AddAsync(identity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Identity identity)
        {
            _context.Identity.Update(identity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var identity = await _context.Identity.FindAsync(id);
            if (identity != null)
            {
                _context.Identity.Remove(identity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
