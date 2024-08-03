using Domain.Accounts.Model;
namespace Domain.Accounts.Repository;

public interface IReceiptReadOnlyRepository
{
    Task<IEnumerable<Receipt>> GetByMerchantIdAsync(Guid merchantId);
    Task<IEnumerable<Receipt>> GetByDateRangeAsync(DateTime start, DateTime end);
}
