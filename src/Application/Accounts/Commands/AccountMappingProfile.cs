using Application.Accounts.Commands.CreateAccount;
using Application.Accounts.Commands.CreateTransaction;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Domain.Common.ValueObjects;
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

        CreateMap<ReceiptDto, Receipt>()
            .ConstructUsing((dto, ctx) =>
            {
                var merchant = ctx.Mapper.Map<Merchant>(dto.Merchant);
                var paymentMethod = ctx.Mapper.Map<PaymentMethodInfo>(dto.PaymentMethod);
                var recipt = Receipt.Create(
                    id: Guid.NewGuid(),
                    merchant: merchant,
                    paymentMethod: paymentMethod
                );
                foreach(var item in dto.Items)
                {
                    var receiptItem = ctx.Mapper.Map<ReceiptItem>(item);
                    recipt.AddReceiptItem(receiptItem);
                }
                return recipt;
            });

        CreateMap<MerchantDto, Merchant>()
            .ConstructUsing((dto, ctx) =>
            {
                var address = new Address(
                    country: dto.Country,
                    city: dto.City,
                    street: dto.Street
                );
                return Merchant.Create(
                    name: dto.Name,
                    address: address,
                    phoneNumber: dto.PhoneNumber
                );
            });
        CreateMap<PaymentMethodDto, PaymentMethodInfo>()
            .ConstructUsing((dto, ctx) =>
            {
                return PaymentMethodInfo.Create(
                    type: dto.Type,
                    last4: dto.Last4
                );
            });
        CreateMap<ItemDto, ReceiptItem>()
            .ConstructUsing((dto, ctx) =>
            {
                return new ReceiptItem(
                    itemName: dto.ItemName,
                    itemDescription: dto.ItemDescription,
                    quantity: dto.Quantity,
                    unit: dto.Unit,
                    unitPrice: dto.UnitPrice,
                    totalPrice: dto.TotalPrice,
                    taxRate: dto.TaxRate,
                    category: dto.Category
                    );
            });
    }
}
