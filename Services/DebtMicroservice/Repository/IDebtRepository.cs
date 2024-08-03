using DebtMicroservice.Models;
namespace DebtMicroservice.Repository;
public interface IDebtRepository
{
    Task<Debt> GetByIdAsync(int id);
    Task<IEnumerable<Debt>> GetAllAsync();
    Task AddAsync(Debt debt);
    Task UpdateAsync(Debt debt);
    Task DeleteAsync(int id);
}
