using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using SharedKernel.Abstractions;

namespace Infrastructure.Data.Accounts;

public class AccountRepository(IDbOperations context, IUnitOfWork unitOfWork) : Repository<Account>(context, unitOfWork), IAccountRepository
{

    public Task AddReceiptAsync(Guid accountId, Guid transactionId, Receipt receipt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddTransactionAsync(Guid accountId, Transaction transaction, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Create(Account account, CancellationToken cancellationToken)
    {
        await AddAsync(account, cancellationToken);
    }

    public Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}