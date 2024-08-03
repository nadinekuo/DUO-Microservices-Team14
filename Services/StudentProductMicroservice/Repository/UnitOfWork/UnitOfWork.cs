using StudentProductMicroservice.Models;

namespace StudentProductMicroservice.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
    public IGrantRepository GrantsRepository { get; private set; }
    public ILoanRepository LoansRepository { get; private set; }
        private readonly StudentProductContext _dbContext;

        public UnitOfWork(StudentProductContext dbContext,
IGrantRepository grantsRepo, ILoanRepository loansRepo)

        {
            _dbContext = dbContext;
            GrantsRepository = grantsRepo;
            LoansRepository = loansRepo;
        }

        public void Commit()
        => _dbContext.SaveChanges();
        public async Task CommitAsync()
        => await _dbContext.SaveChangesAsync();

        public async Task DisposeAsync()
        => await _dbContext.DisposeAsync();

        public void Dispose()
        => _dbContext.Dispose();

    }
}