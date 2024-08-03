using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan> GetLoanByIdAsync(int id);
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<bool> UpdateLoanAsync(int id, Loan loan);
        Task<bool> DeleteLoanAsync(int id);
        Task<string> SendPayoutToTransactionMicroservice(Payout payout);

    }
}