using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DbSet<Profile> _profiles;
    private readonly SocialContext _context;

    public ProfileRepository(SocialContext context)
    {
        _profiles = context.Profiles;
        _context = context;
    }

    public Task<Profile?> GetByUserId(Guid id)
    {
        return _profiles.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task InsertAsync(Profile profile)
    {
        await _profiles.AddAsync(profile);
    }
}
