using Application.Members.Commands;
using AutoMapper;
using Domain.Members.Model;
using Domain.Members.ValueObjects;

namespace Application.Members.Factories;

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

                // Create and add profile
                member.AddProfile(
                    cmd.Name,
                    cmd.Surname,
                    new EducationInformation((EducationLevelCode)cmd.educationLevelCode),
                    new EmploymentInformation(
                        (EmploymentIndustry)cmd.employmentEmploymentIndustryCode,
                        (EmploymentStatus)cmd.employmentStatusCode
                    ),
                    new Address(cmd.Street, cmd.City, cmd.Country),
                    new ResidentialInformation(
                        (ResidantialStatus)cmd.residentialStatusCode,
                        (ResidantialType)cmd.residentialTypeCode
                    ),
                    new FamilySize(cmd.FamilySize)
                );

                return member;
            });


        CreateMap<CreateMemberCommand, MemberProfile>()
            .ConstructUsing((cmd, ctx) =>
            {
                var member = ctx.Mapper.Map<Member>(cmd);
                return member.MemberProfile;
            });
    }
}
