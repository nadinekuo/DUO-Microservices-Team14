namespace StudentProductMicroservice.Models;

public class BasicGrant: Grant
{
    public BasicGrant(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }

    
}
