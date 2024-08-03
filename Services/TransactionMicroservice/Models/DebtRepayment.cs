using System.ComponentModel.DataAnnotations;

namespace TransactionMicroservice.Models
{
    public class DebtRepayment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal InterestAmount { get; set; }

        public DebtRepayment(int studentID, DateTime date, decimal amount, decimal interestAmount)
        {
            StudentID = studentID;
            Date = date;
            Amount = amount;
            InterestAmount = interestAmount;
        }
    }
}