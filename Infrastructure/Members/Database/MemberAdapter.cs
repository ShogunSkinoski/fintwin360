using Domain.Members.Model;
using Domain.Members.Port;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Infrastructure.Members.Database;

public class MemberAdapter(IDbOperations context, IUnitOfWork unitOfWork) : Repository<Member>(context, unitOfWork), MemberPort
{
    public async Task Create(Member member, CancellationToken ct)
    {
        await AddAsync(member, ct);
    }

    public async Task<Member> Retrive(Guid id, CancellationToken ct)
    {
        return await GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Member with id {id} not found.");
    }

    public async Task<Member?> Retrive(string email, CancellationToken ct)
    {
        return await _context.Set<Member>()
            .FirstOrDefaultAsync(m => m.Email == email, ct);
    }
}
