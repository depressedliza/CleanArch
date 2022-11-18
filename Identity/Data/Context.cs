using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public class Context : IdentityDbContext<AppUser>
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }
}
