using Domain.Members.Model;

namespace Domain.Members.Port;

public interface MemberPort
{
    public Task Create(Member member, CancellationToken ct);
    public Task<Member> Retrive(Guid id, CancellationToken ct);
    public Task<Member?> Retrive(string email, CancellationToken ct);
}
