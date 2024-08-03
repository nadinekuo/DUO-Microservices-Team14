public class GrantCreationRequest
{
    public string GrantType { get; set; }
    public int StudentId { get; set; }
    public decimal MonthlyAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
