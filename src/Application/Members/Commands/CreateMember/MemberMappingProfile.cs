using AutoMapper;
using Domain.Members.Model;
using Domain.Members.ValueObjects;

namespace Application.Members.Commands.CreateMember;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<CreateMemberCommand, Member>()
            .ConstructUsing((cmd, ctx) =>
            {
                var member = Member.Create(
                    Guid.NewGuid(),
                    cmd.Email,
                    cmd.Password,
                    (AccountType)cmd.AccountType
                );

                member.AddProfile(
                    cmd.Name,
                    cmd.Surname,
                    new EducationInformation((EducationLevelCode)cmd.EducationLevelCode),
                    new EmploymentInformation(
                        (EmploymentIndustry)cmd.EmploymentIndustryCode,
                        (EmploymentStatus)cmd.EmploymentStatusCode
                    ),
                    new Address(cmd.Street, cmd.City, cmd.Country),
                    new ResidentialInformation(
                        (ResidantialStatus)cmd.ResidentialStatusCode,
                        (ResidantialType)cmd.ResidentialTypeCode
                    ),
                    new FamilySize(cmd.FamilySize)
                );

                return member;
            });
    }
}