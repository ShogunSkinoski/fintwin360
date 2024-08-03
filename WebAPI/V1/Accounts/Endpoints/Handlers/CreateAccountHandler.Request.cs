using Application.Accounts.Commands.CreateAccount;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

public sealed record CreateAccountHandlerRequest
    (
        Guid AccountId,
        string AccountName,
        bool IsPersonel
    )
{
    public CreateAccountCommand ToCommand() => new CreateAccountCommand(
                AccountId,
                AccountName,
                IsPersonel
           );
}
