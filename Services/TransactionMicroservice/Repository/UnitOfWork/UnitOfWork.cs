using TransactionMicroservice.Models;
using TransactionMicroservice.Repository;

namespace TransactionMicroservice.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDebtRepaymentRepository DebtRepaymentRepository { get; }
        public IPayoutRepository PayoutRepository { get; }
        private readonly TransactionContext _dbContext;

        public UnitOfWork(TransactionContext dbContext, IDebtRepaymentRepository debtRepaymentsRepo, IPayoutRepository payoutsRepo)

        {
            _dbContext = dbContext;
            DebtRepaymentRepository = debtRepaymentsRepo;
            PayoutRepository = payoutsRepo;
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