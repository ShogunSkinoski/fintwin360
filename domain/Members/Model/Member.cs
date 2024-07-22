using Domain.Members.ValueObjects;
using SharedKernel.Common;
using SharedKernel.primitives;

namespace Domain.Members.Model;

public class Member : AggregateRoot
{
    private Member(Guid id, string email, string password, AccountType accountType) : base(id)
    {
        Email = email;
        Password = password;
        AccountType = accountType;
    } 
    public string Email { get; private set; }
    public string Password { get; private set; }
    public AccountType AccountType { get; private set; }

    public static Member Create(Guid id, string email, string password, AccountType accountType)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(password);

        var emailResult = GuardClause.Ensure.IsValidEmail(email);

        if (!emailResult.IsSuccess) throw new ArgumentException(emailResult.Reasons[0].ToString());

        return new Member(id, email, password, accountType);
    }
    
}
