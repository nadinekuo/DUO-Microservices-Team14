using StudentProductMicroservice.Models;

public interface ILoanFactory
{
    public Loan CreateLoan(string loanType, int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate);

}
