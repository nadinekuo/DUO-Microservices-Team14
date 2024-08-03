using System.ComponentModel.DataAnnotations;

namespace StudentProductMicroservice.Models
{
    public abstract class StudentProduct
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public decimal MonthlyAmount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Dummy methods for now
        public virtual void Deactivate()
        {
            IsActive = false;
        }


        public virtual void Activate()
        {
            IsActive = true;
        }
        
        public virtual void SetMonthlyAmount(decimal amount)
        {
            MonthlyAmount = amount;
        }

        public virtual void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }

        public virtual void SendMail()
        {
        }

        public StudentProduct(int studentId, decimal monthlyAmount, DateTime startDate, DateTime endDate)
        {
            StudentId = studentId;
            MonthlyAmount = monthlyAmount;
            IsActive = false;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
