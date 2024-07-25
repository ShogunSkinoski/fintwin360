using SharedKernel.Abstractions;
using Application.Members.Commands;
using FluentResults;
using AutoMapper;
using Domain.Members.Model;

namespace Application.Members.UseCases;

public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    public CreateMemberCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<Result<Guid>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {

        var member = _mapper.Map<Member>(command);
        return Result.Ok(member.Id);
    }
}
