using IdentityMicroservice.Models;
namespace IdentityMicroservice.Repository;
public interface IIdentityRepository
{
    Task<Identity> GetByIdAsync(int id);
    Task<IEnumerable<Identity>> GetAllAsync();
    Task AddAsync(Identity identity);
    Task UpdateAsync(Identity identity);
    Task DeleteAsync(int id);
}