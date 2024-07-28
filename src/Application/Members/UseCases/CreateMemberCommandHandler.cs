using SharedKernel.Abstractions;
using Application.Members.Commands;
using FluentResults;
using AutoMapper;
using Domain.Members.Model;
using FluentValidation;
using Domain.Members.Port;

namespace Application.Members.UseCases;

public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly MemberPort _memberPort;

    private readonly IValidator<CreateMemberCommand> _validator;

    private readonly IMapper _mapper;
    public CreateMemberCommandHandler(
        IUnitOfWork unitOfWork,
        MemberPort memberPort,
        IMapper mapper,
        IValidator<CreateMemberCommand> validator
        )
    {
        _unitOfWork = unitOfWork;
        _memberPort = memberPort;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<Result<Guid>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result.Fail<Guid>(validationResult.Errors.Select(e => new Error(e.ErrorMessage)));
        }
        var member = _mapper.Map<Member>(command);

        await _memberPort.Create(member, cancellationToken);
        
        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (saveResult == 0)
        {
            return Result.Fail<Guid>("Failed to save member");
        }

        return Result.Ok(member.Id);
    }
}
