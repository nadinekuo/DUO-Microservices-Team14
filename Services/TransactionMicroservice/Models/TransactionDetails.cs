namespace TransactionMicroservice.Models
{
    // DTO that can be used for all transactions. 
    // Maybe put in separate DTO folder
    public class TransactionDetails
    {

        public int StudentID { get; set; }
        public int ProductID { get; set; }
        public string PayeeIdentifier { get; set; }
        public string PayerIdentifier { get; set; }
        public decimal Amount { get; set; }
        public string PaymentDescription { get; set; }
        public DateTime Date { get; set; }

        public TransactionDetails(int studentID, int productID, string payeeIdentifier, string payerIdentifier, decimal amount, string paymentDescription, DateTime date)
        {
            StudentID = studentID;
            ProductID = productID;
            PayeeIdentifier = payeeIdentifier;
            PayerIdentifier = payerIdentifier;
            Amount = amount;
            PaymentDescription = paymentDescription;
            Date = date;
        }
    }
}