using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repository
{
    public class DebtRepaymentRepository : IDebtRepaymentRepository
    {
        private readonly TransactionContext _context;

        public DebtRepaymentRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DebtRepayment>> GetAllAsync()
        {
            return await _context.DebtRepayments.ToListAsync();
        }

        public async Task<DebtRepayment> GetByIdAsync(int id)
        {
            return await _context.DebtRepayments.FindAsync(id);
        }

        public async Task AddAsync(DebtRepayment debtRepayment)
        {
            _context.DebtRepayments.Add(debtRepayment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DebtRepayment debtRepayment)
        {
            _context.DebtRepayments.Update(debtRepayment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var debtRepayment = await _context.DebtRepayments.FindAsync(id);
            if (debtRepayment != null)
            {
                _context.DebtRepayments.Remove(debtRepayment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
