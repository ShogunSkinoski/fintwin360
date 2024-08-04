using Domain.Accounts.Model;

namespace Domain.Accounts.Repository;

public interface IAccountRepository
{
    Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
    Task Create(Account account, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task AddTransactionAsync(Guid accountId, Transaction transaction, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);

    Task AddReceiptAsync(Guid accountId, Guid transactionId, Receipt receipt, CancellationToken cancellationToken);
    Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken);
    Task UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken);
}