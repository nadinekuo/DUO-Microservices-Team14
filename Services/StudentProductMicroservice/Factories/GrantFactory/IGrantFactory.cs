using StudentProductMicroservice.Models;
namespace StudentProductMicroservice.Factories;
public interface IGrantFactory {
    public Grant CreateGrant(string grantType, int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate, bool isWeek=false);
}