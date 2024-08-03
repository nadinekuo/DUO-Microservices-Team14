namespace DebtMicroservice.Models
{
    public class Debt
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        public decimal CurrTotal { get; set; }
        public decimal InterestRate { get; set;}

        //dummy methods for now
        public virtual void SetCurrTotal(decimal total)
        {
            CurrTotal = total;
        }

        public virtual void SetInterestRate(decimal rate)
        {
            InterestRate = rate;
        }

        public Debt(int studentId, decimal currTotal, decimal interestRate)
        {
            StudentId = studentId;
            CurrTotal = currTotal;
            InterestRate = interestRate;
        }
    }
}