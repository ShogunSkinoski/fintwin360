using Domain.Members.Model;
using Domain.Members.ValueObjects;
using TestHelpers;

namespace domain.test.members.model;

public class MemberTest 
{
    [Theory]
    [InlineData(AccountType.Standard, "test@gmail.com","test123.")]
    public void ShouldCreate_Member_WithCorrectParameters(AccountType accountType,string email, string password)
    {
        // Given
        var id = Guid.NewGuid();
        // When
        var member = Member.Create(id, email, password, accountType);

        // Then
        Assert.Equal(id, member.Id);
    }
}
