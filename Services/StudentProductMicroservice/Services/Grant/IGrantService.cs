using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Services
{
    public interface IGrantService
    {
        Task<IEnumerable<Grant>> GetAllGrantsAsync();
        Task<Grant> GetGrantByIdAsync(int id);
        Task<Grant> CreateGrantAsync(Grant grant);
        Task<bool> UpdateGrantAsync(int id, Grant grant);
        Task<bool> DeleteGrantAsync(int id);

        Task<string> SendPayoutToTransactionMicroservice(Payout payout);
    }
}