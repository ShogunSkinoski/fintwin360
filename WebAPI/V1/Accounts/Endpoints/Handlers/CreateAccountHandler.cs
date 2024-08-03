using Application.Accounts.Commands.CreateAccount;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

internal static partial class AccountEndpoints
{
    public static async Task<IResult> CreateAccountHandler(
        CreateAccountHandlerRequest request,
        ICommandPublisher publisher,
        CancellationToken cancellationToken
    )
    {
        Result<Guid> result = await publisher.Publish<Result<Guid>, CreateAccountCommand>(request.ToCommand(), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}
