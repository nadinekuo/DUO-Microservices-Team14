using StudentProductMicroservice.Factories;
using StudentProductMicroservice.Models;

public class GrantFactory : IGrantFactory
{
    public Grant CreateGrant(string grantType, int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate, bool isWeek = false)
    {
        switch (grantType)
        {
            case "BasicGrant":
                return new BasicGrant(studentId, monthlyAmount, startDate, endDate);
            case "SupplementaryGrant":
                return new SupplementaryGrant(studentId, monthlyAmount, startDate, endDate);
            case "TravelProduct":
                return new TravelProduct(studentId, monthlyAmount, startDate, endDate, isWeek);
            default:
                throw new ArgumentException("Invalid grant type");
        }
    }
    
}
