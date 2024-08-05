using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Domain.Accounts.ValueObjects;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;


namespace Infrastructure.Data.Accounts;

public class AccountRepository(IDbOperations context, IUnitOfWork unitOfWork) : Repository<Account>(context, unitOfWork), IAccountRepository
{
    public async Task Create(Account account, CancellationToken cancellationToken)
    {
        await AddAsync(account, cancellationToken);
    }

    public async Task Create(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Set<Transaction>().AddAsync(transaction, cancellationToken);
    }

    public async Task Create(Merchant merchant, CancellationToken cancellationToken)
    {
        await _context.Set<Merchant>().AddAsync(merchant, cancellationToken);
    }
    public async Task Create(Receipt receipt, CancellationToken cancellationToken)
    {
        await _context.Set<Receipt>().AddAsync(receipt, cancellationToken);
    }
    public async Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _context.Set<Account>()
          .Include(a => a.Transactions)
          .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (account == null)
        {
            throw new Exception($"Account with id {id} not found.");
        }

        account.CalculateBalance();
        return account;
    }
    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Transaction>()
                .Where(t =>
                    t.AccountId == accountId &&
                    (
                    (startDate == null ? true : startDate <= t.CreatedAt) &&
                    (endDate == null ? true : endDate >= t.CreatedAt)
                    ) &&
                    (transactionType == null ? true : t.TransactionType == transactionType)
                )
                .Include(t => t.Receipt)
                    .ThenInclude(r => r.PaymentMethod)
                .Include(t => t.Receipt)
                    .ThenInclude(r => r.Merchant)
                .Include(t => t.Receipt)
                    .ThenInclude(r => r.Items)
                 .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
    }

    public Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Account account,CancellationToken cancellationToken)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));

        var entry = _context.Set<Account>().Entry(account);
        entry.State = EntityState.Modified;

        var dbTransactions = await GetTransactionsByAccountIdAsync(account.Id, null,null,null,cancellationToken);

        foreach (var transaction in account.Transactions)
        {
            var dbTransaction = dbTransactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (dbTransaction == null)
            {
                await Create(transaction, cancellationToken);
            }
            else
            {
                _context.Set<Transaction>().Entry(dbTransaction).CurrentValues.SetValues(transaction);
            }
        }

        var receipts = account.Transactions
                              .Select(t => t.Receipt)
                              .Where(r => r != null)
                              .ToList();

        var merchants = receipts
                    .Where(r => r != null)
                    .Select(r => r.Merchant)
                    .Where(m => m != null)
                    .ToList();

        foreach (var merchant in merchants)
        {
            var dbMerchant = await _context.Set<Merchant>().FirstOrDefaultAsync(m => m.Id == merchant.Id, cancellationToken);
            if (dbMerchant == null)
            {
                await Create(merchant, cancellationToken);
            }
            else
            {
                _context.Set<Merchant>().Entry(dbMerchant).CurrentValues.SetValues(merchant);
            }
        }

        foreach (var receipt in receipts)
        {
            var dbReceipt = await _context.Set<Receipt>().FirstOrDefaultAsync(r => r.Id == receipt.Id, cancellationToken);
            if (dbReceipt == null)
            {
                await Create(receipt, cancellationToken);
            }
            else
            {
                _context.Set<Receipt>().Entry(dbReceipt).CurrentValues.SetValues(receipt);
            }
        }

        foreach (var dbTransaction in dbTransactions)
        {
            if (!account.Transactions.Any(t => t.Id == dbTransaction.Id))
            {
                _context.Set<Transaction>().Remove(dbTransaction);
            }
        }

        account.CalculateBalance();


        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    
    public Task UpdateAsync(Receipt receipt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}