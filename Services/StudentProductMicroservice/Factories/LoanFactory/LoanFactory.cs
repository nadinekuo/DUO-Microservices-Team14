using StudentProductMicroservice.Models;

public class LoanFactory : ILoanFactory
{
    public Loan CreateLoan(string loanType, int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate)
    {
        switch (loanType)
        {
            case "InterestBearingLoan":
                return new InterestBearingLoan(studentId, monthlyAmount, startDate, endDate);
            case "TuitionFeesLoan":
                return new TuitionFeesLoan(studentId, monthlyAmount, startDate, endDate);
            default:
                throw new ArgumentException("Invalid loan type");
        }
    }
}
