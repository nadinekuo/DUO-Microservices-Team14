using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repository
{
    public interface IDebtRepaymentRepository
    {
        Task<IEnumerable<DebtRepayment>> GetAllAsync();
        Task<DebtRepayment> GetByIdAsync(int id);
        Task AddAsync(DebtRepayment debtRepayment);
        Task UpdateAsync(DebtRepayment debtRepayment);
        Task DeleteAsync(int id);
    }
}
