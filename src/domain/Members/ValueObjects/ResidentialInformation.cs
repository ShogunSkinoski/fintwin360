using SharedKernel.Primitives;

namespace Domain.Members.ValueObjects
{

    public enum ResidantialStatus
    {
        HomeOwner,
        Renter,
        Other
    }

    public enum ResidantialType
    {
        House,
        Apartment,
        Condo,
        Other
    }
    public sealed record ResidentialInformation
    {
        public ResidentialInformation(ResidantialStatus status, ResidantialType type)
        {
            Status = status;
            Type = type;
        }

        public ResidantialStatus Status { get; private set; }
        public ResidantialType Type { get; private set; }
    }
}