using Microsoft.EntityFrameworkCore;
using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Repository;

public class GrantRepository : IGrantRepository
{
    private readonly StudentProductContext _context;

    public GrantRepository(StudentProductContext context)
    {
        _context = context;
    }

    public async Task<Grant> GetByIdAsync(int id)
    {

        return await _context.Grants.FindAsync(id);
    }

    public async Task<IEnumerable<Grant>> GetAllAsync() 
    {

        return await _context.Grants.ToListAsync();
    }

    public async Task AddAsync(Grant grant)
    {
        await _context.Grants.AddAsync(grant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Grant grant)
    {
        _context.Grants.Update(grant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var grant = await _context.Grants.FindAsync(id);
        if (grant != null)
        {
            _context.Grants.Remove(grant);
            await _context.SaveChangesAsync();
        }
    }
}
