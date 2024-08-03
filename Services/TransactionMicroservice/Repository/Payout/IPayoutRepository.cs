using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionMicroservice.Models;
namespace TransactionMicroservice.Repository
{
    public interface IPayoutRepository
    {
        Task<IEnumerable<Payout>> GetAllAsync();
        Task<Payout> GetByIdAsync(int id);
        Task AddAsync(Payout payout);
        Task UpdateAsync(Payout payout);
        Task DeleteAsync(int id);
    }
}
