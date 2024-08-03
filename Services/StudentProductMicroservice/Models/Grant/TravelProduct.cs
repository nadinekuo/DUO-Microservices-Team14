using System.ComponentModel.DataAnnotations;

namespace StudentProductMicroservice.Models;
public class TravelProduct : Grant
{

    [Required]
    public bool IsWeek { get; set; }

    public TravelProduct(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate, bool IsWeek) : base(studentId, monthlyAmount, startDate, endDate)
    {
        IsWeek = IsWeek;
    }

}