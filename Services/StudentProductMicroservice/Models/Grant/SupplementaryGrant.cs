namespace StudentProductMicroservice.Models;

public class SupplementaryGrant : Grant
{
    public SupplementaryGrant(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }
}
