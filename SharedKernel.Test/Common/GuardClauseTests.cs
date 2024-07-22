using FluentAssertions;
using SharedKernel.Common;

public class GuardClauseTests
{
    public static IEnumerable<object[]> ValidEmailArguments => new List<object[]>
    {
        new object [] {"test@example.com", true },
        new object [] {"user.name+tag+sorting@example.com", false},
        new object [] {"user@subdomain.example.com", true},
        new object [] {"user.name@subdomain.example.com", true},
        new object [] {"plainaddress", false},
        new object [] {"@missingusername.com", false},
        new object [] {"username@.com.my", false},
        new object [] {"username@.com", false},
        new object [] {"", false},
        new object [] {null, false},
        new object [] {" ", false},
    };
    [Theory]
    [MemberData(nameof(ValidEmailArguments))]
    public void IsValidEmail_Should_Return_Correct_Result(string email, bool isValid)
    {
        // Act
        var result = GuardClause.Ensure.IsValidEmail(email);

        // Assert
        if (isValid)
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(email);
        }
        else
        {
            result.IsFailed.Should().BeTrue();
        }
    }
}
