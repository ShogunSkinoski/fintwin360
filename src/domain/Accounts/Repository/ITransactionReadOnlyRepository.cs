using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;

namespace Domain.Accounts.Repository;

public interface ITransactionReadOnlyRepository
{
    Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime start, DateTime end);
    Task<IEnumerable<Transaction>> GetByTypeAsync(TransactionType type);
}
