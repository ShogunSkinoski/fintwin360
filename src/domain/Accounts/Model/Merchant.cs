using Domain.Common.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;

public class Merchant : Entity, IDeletableEntity, IAuditableEntity
{
    protected Merchant() : base(new Guid()) { }
    public Merchant(Guid id, string name, Address address, string phoneNumber)
        : base(id)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
    }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public string PhoneNumber { get; private set; }

    public bool IsDeleted => throw new NotImplementedException();

    public DateTime? DeletedAt => throw new NotImplementedException();

    public DateTime CreatedAt => throw new NotImplementedException();

    public DateTime? UpdatedAt => throw new NotImplementedException();
}
