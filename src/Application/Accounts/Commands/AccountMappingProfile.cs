using Application.Accounts.Commands.CreateAccount;
using Application.Accounts.Commands.CreateTransaction;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Domain.Members.Model;
using Domain.Members.Repository;
using Microsoft.VisualBasic;

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
