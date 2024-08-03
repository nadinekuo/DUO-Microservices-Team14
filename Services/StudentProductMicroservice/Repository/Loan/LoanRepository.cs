using Microsoft.EntityFrameworkCore;
using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly StudentProductContext _context;

    public LoanRepository(StudentProductContext context)
    {
        _context = context;
    }

    public async Task<Loan> GetByIdAsync(int id)
    {

        return await _context.Loans.FindAsync(id);
    }

    public async Task<IEnumerable<Loan>> GetAllAsync() 
    {

        return await _context.Loans.ToListAsync();
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan != null)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }
}
