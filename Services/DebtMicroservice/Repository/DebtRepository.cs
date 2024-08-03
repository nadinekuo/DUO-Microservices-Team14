using Microsoft.EntityFrameworkCore;
using DebtMicroservice.Models;
namespace DebtMicroservice.Repository;

public class DebtRepository : IDebtRepository
{
    private readonly DebtContext _context;

    public DebtRepository(DebtContext context)
    {
        _context = context;
    }

    public async Task<Debt> GetByIdAsync(int id)
    {
        return await _context.Debt.FindAsync(id);
    }

    public async Task<IEnumerable<Debt>> GetAllAsync() 
    {
        return await _context.Debt.ToListAsync();
    }

    public async Task AddAsync(Debt debt)
    {
        await _context.Debt.AddAsync(debt);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Debt debt)
    {
        _context.Debt.Update(debt);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var debt = await _context.Debt.FindAsync(id);
        if (debt != null)
        {
            _context.Debt.Remove(debt);
            await _context.SaveChangesAsync();
        }
    }
}