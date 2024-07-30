using Application.Members.Commands;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Members.Endpoints.Handlers;

public static partial class MemberEndpoints 
{ 
    public static async Task<IResult> CreateMemberHandler(
            CreateMemberHandlerRequest request,
            ICommandPublisher publisher,
            CancellationToken cancellationToken
        )
    {
        Result<Guid> result = await publisher.Publish<Result<Guid>, CreateMemberCommand>(request.ToCommand(), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}