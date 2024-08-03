public class LoanCreationRequest
{
    public string LoanType { get; set; }
    public int StudentId { get; set; }
    public decimal MonthlyAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
