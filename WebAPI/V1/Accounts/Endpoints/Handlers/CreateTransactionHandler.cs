using Application.Accounts.Commands.CreateTransaction;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

internal static partial class AccountEndpoints
{
    public static async Task<IResult> CreateTransactionHandler(
            CreateTransactionHandlerRequest request,
            ICommandPublisher publisher,
            CancellationToken cancellationToken
        )
    {
        Result<Guid> result = await publisher.Publish<Result<Guid>, CreateTransactionCommand>(request.ToCommand(), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}
