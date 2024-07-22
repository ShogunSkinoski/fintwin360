
namespace SharedKernel.primitives;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id)
        : base(id)
    {
    }
}
