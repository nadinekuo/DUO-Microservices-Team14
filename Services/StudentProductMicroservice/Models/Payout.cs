namespace StudentProductMicroservice.Models;

    public class Payout
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int ProductID { get; set; }
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public Payout(int studentID, int productID, DateTime date, decimal amount)
        {
            StudentID = studentID;
            Date = date;
            Amount = amount;
            ProductID = productID;
        }
    }
