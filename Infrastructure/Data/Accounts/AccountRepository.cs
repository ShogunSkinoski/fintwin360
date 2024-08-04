using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;
using System.Threading;

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

    public Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _context.Set<Transaction>()
                .Where(t => t.AccountId == accountId)
                .ToListAsync(cancellationToken);
    }

    public async Task UpdateAccountAsync(Account account,CancellationToken cancellationToken)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));

        var entry = _context.Set<Account>().Entry(account);
        entry.State = EntityState.Modified;

        var dbTransactions = await GetTransactionsByAccountIdAsync(account.Id, cancellationToken);

        foreach (var transaction in account.Transactions)
        {
            var dbTransaction = dbTransactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (dbTransaction == null)
            {
                _context.Set<Transaction>().Add(transaction);
            }
            else
            {
                _context.Set<Transaction>().Entry(dbTransaction).CurrentValues.SetValues(transaction);
            }
        }

        // Remove transactions that are in the database but not in the current account object
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
}