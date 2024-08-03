namespace StudentProductMicroservice.Models;
public class InterestBearingLoan : Loan
{
    public InterestBearingLoan(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }

}