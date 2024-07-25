using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Members.Commands;

public sealed record CreateMemberCommand (
    string Name,
    string Surname,
    string Email,
    string Password,
    string Country,
    string City,
    string Street,
    int AccountType,
    int educationLevelCode,
    int employmentStatusCode,
    int employmentEmploymentIndustryCode,
    int residentialStatusCode,
    int residentialTypeCode,
    int FamilySize
    ) : ICommand<Result<Guid>>;