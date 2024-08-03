using TransactionMicroservice.Models;

namespace TransactionMicroservice.Services
{
    public interface IPayoutService
    {
        Task<IEnumerable<Payout>> GetAllPayoutsAsync();
        Task<Payout> GetPayoutByIdAsync(int id);
        Task<Payout> CreatePayoutAsync(Payout payout);
        Task<bool> UpdatePayoutAsync(int id, Payout payout);
        Task<bool> DeletePayoutAsync(int id);
        Task<TransactionResult> SendPaymentAsync(TransactionDetails transactionDetails);
    }
}