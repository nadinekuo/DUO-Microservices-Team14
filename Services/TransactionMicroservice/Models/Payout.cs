using System.ComponentModel.DataAnnotations;

namespace TransactionMicroservice.Models
{
    public class Payout
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Payout(int studentID, int productID, DateTime date, decimal amount)
        {
            StudentID = studentID;
            Date = date;
            Amount = amount;
            ProductID = productID;
        }
    }
}