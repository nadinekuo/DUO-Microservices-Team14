using TransactionMicroservice.Models;

namespace TransactionMicroservice.Services
{
    public interface IDebtRepaymentService
    {
        Task<IEnumerable<DebtRepayment>> GetAllDebtRepaymentsAsync();
        Task<DebtRepayment> GetDebtRepaymentByIdAsync(int id);
        Task<DebtRepayment> CreateDebtRepaymentAsync(DebtRepayment debtRepayment);
        Task<bool> UpdateDebtRepaymentAsync(int id, DebtRepayment debtRepayment);
        Task<bool> DeleteDebtRepaymentAsync(int id);
    }
}