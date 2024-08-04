using Application.Accounts.Commands.CreateTransaction;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Domain.Accounts.ValueObjects;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Result<Guid>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IValidator<CreateTransactionCommand> _validator;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(
        IAccountRepository accountRepository,
        IValidator<CreateTransactionCommand> validator,
        IMapper mapper
    )
    {
        _accountRepository = accountRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Fail<Guid>(validationResult.Errors.Select(e => new Error(e.ErrorMessage)));
        }

        var account = await _accountRepository.GetAccountByIdAsync(command.AccountId, cancellationToken);

        if (account == null)
        {
            return Result.Fail<Guid>("Account could not found");
        }
        
        if(command.TransactionType == TransactionType.Income)
        {
            var incomeTransaction = Transaction.CreateIncome(
                id: Guid.NewGuid(),
                account: account,
                amount: command.Amount,
                description: command.Description
                );
            account.AddTransaction(incomeTransaction);
            await _accountRepository.UpdateAccountAsync(account, cancellationToken);
            return Result.Ok(incomeTransaction.Id);
        }
        return Result.Fail<Guid>("Transaction type is not valid");
    }
}
