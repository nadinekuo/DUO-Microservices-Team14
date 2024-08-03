namespace StudentProductMicroservice.Repository {
public interface IUnitOfWork : IDisposable
{

    IGrantRepository GrantsRepository { get; }
    ILoanRepository LoansRepository { get; }
    void Commit();
    Task CommitAsync();

    Task DisposeAsync();

}
}