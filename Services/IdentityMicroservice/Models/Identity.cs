namespace IdentityMicroservice.Models
{
    public class Identity
    {
        public int ID { get; set; }
        public int BSN { get; set; }
        public DateTime HigherEducationStartDate { get; set; }
        public DateTime GrantStartDate { get; set; }
        public string BankAccount { get; set; }
        public bool LivesWithParents { get; set; }

        // Dummy methods for now
        public virtual void SetHigherEducationStartDate(DateTime startDate)
        {
            HigherEducationStartDate = startDate;
        }

        public virtual void SetGrantStartDate(DateTime startDate)
        {
            GrantStartDate = startDate;
        }

        public virtual void SetBankAccount(string account)
        {
            BankAccount = account;
        }

        public virtual void SetLivesWithParents(bool livesWithParents)
        {
            LivesWithParents = livesWithParents;
        }

        public Identity(int bsn, DateTime higherEducationStartDate, DateTime grantStartDate, string bankAccount, bool livesWithParents)
        {
            BSN = bsn;
            HigherEducationStartDate = higherEducationStartDate;
            GrantStartDate = grantStartDate;
            BankAccount = bankAccount;
            LivesWithParents = livesWithParents;
        }

        public Identity() //empty constructor needed for pain problems with Pascal Case being expected in constructors. See https://stackoverflow.com/questions/55838188/system-invalidoperationexception-no-suitable-constructor-found-for-entity-type
        {
        }
    }
}