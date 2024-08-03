using Application.Accounts.Commands.CreateAccount;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Members.Model;
using Domain.Members.Repository;

namespace Application.Accounts.Commands;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<CreateAccountCommand, Account>()
            .ConstructUsing((cmd, ctx) =>
            {
                Member member = null!;
                if (ctx.Items.TryGetValue("Member", out var memberObj) && memberObj is Member)
                {
                    member = (Member)memberObj;
                }
                
                return Account.Create(
                    id: Guid.NewGuid(),
                    accountName: cmd.AccountName,
                    isPersonal: cmd.IsPersonal,
                    member: member
                );
            });
    }
}
