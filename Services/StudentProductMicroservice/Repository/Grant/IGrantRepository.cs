using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Repository {
public interface IGrantRepository
{
    Task<IEnumerable<Grant>> GetAllAsync();
    Task<Grant> GetByIdAsync(int id);
    Task AddAsync(Grant grant);
    Task UpdateAsync(Grant grant);
    Task DeleteAsync(int id);
}
}