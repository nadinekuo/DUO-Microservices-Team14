namespace StudentProductMicroservice.Models;
public class TuitionFeesLoan : Loan
{
    // Implement abstract methods here
    public TuitionFeesLoan(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }
}
