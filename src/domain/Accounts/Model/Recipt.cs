using Domain.Accounts.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;

internal class Recipt : AggregateRoot, IDeletableEntity, IAuditableEntity
{
    protected Recipt() : base(new Guid()) { }
    public Recipt(Guid id, Guid accountId, Guid merchantId, IEnumerable<ReciptItem> reciptItems, PaymentMethod paymentMethod)
        : base(id)
    {
        AccountId = accountId;
        MerchantId = merchantId;
        Items = reciptItems;
        PaymentMethod = paymentMethod;
        CalculateTotal();
    }
    public Guid AccountId { get; private set; }
    public Guid MerchantId { get; private set; }
    public IEnumerable<ReciptItem> Items { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public decimal Total { get; private set; }

    public bool IsDeleted => throw new NotImplementedException();

    public DateTime? DeletedAt => throw new NotImplementedException();

    public DateTime CreatedAt => throw new NotImplementedException();

    public DateTime? UpdatedAt => throw new NotImplementedException();

    public void AddReciptItem(ReciptItem reciptItem)
    {
        throw new NotImplementedException();
    }

    public void RemoveReciptItem(ReciptItem reciptItem)
    {
        throw new NotImplementedException();
    }
    
    public void CalculateTotal()
    {
        foreach (var item in Items)
        {
            Total += item.TotalPrice;
        }
    }


}
