using Application.Members.Commands;
using Application.Members.Factories;
using Domain.Members;
using AutoMapper;
using Domain.Members.ValueObjects;

namespace Application.Test.Member.Factories
{
    public class MemberMappingProfileTests
    {
        private readonly IMapper _mapper;

        public MemberMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MemberMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreateMemberCommand_To_Member_Correctly()
        {
            // Arrange
            var command = new CreateMemberCommand(
                "John",
                "Doe",
                "john.doe@example.com",
                "securepassword",
                "Country",
                "City",
                "Street",
                (int)AccountType.Standard,
                (int)EducationLevelCode.Secondary,
                (int)EmploymentStatus.Employed,
                (int)EmploymentIndustry.Manufacturing,
                (int)ResidantialStatus.HomeOwner,
                (int)ResidantialType.House,
                4
            );

            // Act
            var member = _mapper.Map<Domain.Members.Model.Member>(command);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(command.Email, member.Email);
            Assert.Equal(command.Password, member.Password);
            Assert.Equal((AccountType)command.AccountType, member.AccountType);

            Assert.NotNull(member.MemberProfile);
            Assert.Equal(command.Name, member.MemberProfile.Name);
            Assert.Equal(command.Surname, member.MemberProfile.Surname);
            Assert.Equal((EducationLevelCode)command.educationLevelCode, member.MemberProfile.EducationLevel.EducationCode);
            Assert.Equal((EmploymentStatus)command.employmentStatusCode, member.MemberProfile.EmploymentStatus.Status);
            Assert.Equal((EmploymentIndustry)command.employmentEmploymentIndustryCode, member.MemberProfile.EmploymentStatus.EmploymentIndustry);
            Assert.Equal(command.Country, member.MemberProfile.Address.Country);
            Assert.Equal(command.City, member.MemberProfile.Address.City);
            Assert.Equal(command.Street, member.MemberProfile.Address.Street);
            Assert.Equal((ResidantialStatus)command.residentialStatusCode, member.MemberProfile.ResidentialInformation.Status);
            Assert.Equal((ResidantialType)command.residentialTypeCode, member.MemberProfile.ResidentialInformation.Type);
            Assert.Equal(command.FamilySize, member.MemberProfile.FamilySize.Size);
        }
    }
}
