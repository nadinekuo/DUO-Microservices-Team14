namespace StudentProductMicroservice.Models;

public abstract class Grant : StudentProduct
{
    public Grant(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate) : base(studentId, monthlyAmount, startDate, endDate)
    {
    }

    
}