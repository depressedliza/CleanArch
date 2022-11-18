using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IProfileRepository
{
    public Task<Profile?> GetByUserId(Guid id);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task InsertAsync(Profile profile);
}
