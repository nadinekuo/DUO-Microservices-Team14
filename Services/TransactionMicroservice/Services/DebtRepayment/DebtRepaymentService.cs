using TransactionMicroservice.Services;
using TransactionMicroservice.Repository;
using TransactionMicroservice.Models;
public class DebtRepaymentService : IDebtRepaymentService {
    public IUnitOfWork _unitOfWork;

    public DebtRepaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DebtRepayment>> GetAllDebtRepaymentsAsync()
    {
        return await _unitOfWork.DebtRepaymentRepository.GetAllAsync();
    }

    public async Task<DebtRepayment> GetDebtRepaymentByIdAsync(int id)
    {
        var debtRepayment = await _unitOfWork.DebtRepaymentRepository.GetByIdAsync(id);
        if (debtRepayment == null)
        {
            // Handle the case where the debtRepayment is not found
            throw new KeyNotFoundException($"DebtRepayment with ID {id} not found.");
        }
        return debtRepayment;
    }

    public async Task<DebtRepayment> CreateDebtRepaymentAsync(DebtRepayment debtRepayment)
    {
        await _unitOfWork.DebtRepaymentRepository.AddAsync(debtRepayment);
        await _unitOfWork.CommitAsync();
        return debtRepayment;
    }

    public async Task<bool> UpdateDebtRepaymentAsync(int id, DebtRepayment debtRepayment)
    {
        var existingDebtRepayment = await _unitOfWork.DebtRepaymentRepository.GetByIdAsync(id);
        // Handle the case where the debtRepayment does not exist
        if (existingDebtRepayment == null)
        {
            return false;
        }

        await _unitOfWork.DebtRepaymentRepository.UpdateAsync(debtRepayment);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteDebtRepaymentAsync(int id)
    {
        var debtRepayment = await _unitOfWork.DebtRepaymentRepository.GetByIdAsync(id);
        // Handle the case where the debtRepayment does not exist
        if (debtRepayment == null)
        {
            return false;
        }

        await _unitOfWork.DebtRepaymentRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        return true;
    }


    
}
