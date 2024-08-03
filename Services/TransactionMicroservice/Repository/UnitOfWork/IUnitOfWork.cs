namespace TransactionMicroservice.Repository {
public interface IUnitOfWork : IDisposable
{

    IDebtRepaymentRepository DebtRepaymentRepository { get; }
    IPayoutRepository PayoutRepository { get; }
    void Commit();
    Task CommitAsync();

    Task DisposeAsync();

}
}