using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

namespace Persistence;

public class SocialContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Profile> Profiles => Set<Profile>();

    public SocialContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Social") ?? throw new InvalidOperationException();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
