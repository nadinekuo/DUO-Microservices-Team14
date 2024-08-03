using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repository
{
    public class PayoutRepository : IPayoutRepository
    {
        private readonly TransactionContext _context;

        public PayoutRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payout>> GetAllAsync()
        {
            return await _context.Payouts.ToListAsync();
        }

        public async Task<Payout> GetByIdAsync(int id)
        {
            return await _context.Payouts.FindAsync(id);
        }

        public async Task AddAsync(Payout payout)
        {
            _context.Payouts.Add(payout);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payout payout)
        {
            _context.Payouts.Update(payout);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payout = await _context.Payouts.FindAsync(id);
            if (payout != null)
            {
                _context.Payouts.Remove(payout);
                await _context.SaveChangesAsync();
            }
        }
    }
}
