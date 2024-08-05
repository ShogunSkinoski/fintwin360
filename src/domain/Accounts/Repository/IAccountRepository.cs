using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;

namespace Domain.Accounts.Repository;

public interface IAccountRepository
{
    Task Create(Account account, CancellationToken cancellationToken);
    Task Create(Merchant merchant, CancellationToken cancellationToken);
    Task Create(Transaction transaction, CancellationToken cancellationToken);
    Task Create(Receipt receipt, CancellationToken cancellationToken);

    Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>>GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken);
    Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken);


    Task UpdateAsync(Account account, CancellationToken cancellationToken);
    Task UpdateAsync(Receipt receipt, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}