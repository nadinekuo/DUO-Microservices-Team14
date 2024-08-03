namespace StudentProductMicroservice.Models;
public abstract class Loan : StudentProduct
{
    // Additional properties and methods specific to Loan
    public Loan(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }
}