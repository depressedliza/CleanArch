using Identity.Data;
using Identity.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Identity");
var connectionStringServer = builder.Configuration.GetConnectionString("Server");

builder.Services.AddDbContext<Context>(c => c.UseNpgsql(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

var clients = builder.Configuration.GetSection("IdentityServerSettings:Clients").Get<List<Client>>();
var scopes = builder.Configuration.GetSection("IdentityServerSettings:ApiScopes").Get<List<ApiScope>>();
var resource = builder.Configuration.GetSection("IdentityServerSettings:ApiResources").Get<List<ApiResource>>();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
    })
    .AddAspNetIdentity<AppUser>()
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = b => b.UseNpgsql(connectionStringServer,
            sql => sql.MigrationsAssembly("Identity"));
    })
    .AddOperationalStore(opt =>
    {
        opt.ConfigureDbContext = b => b.UseNpgsql(connectionStringServer,
            sql => sql.MigrationsAssembly("Identity"));
        opt.EnableTokenCleanup = true;
    })
    .AddInMemoryApiResources(resource)
    .AddInMemoryIdentityResources(
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        })
    .AddInMemoryApiScopes(scopes)
    .AddInMemoryClients(clients)
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var identity = scope.ServiceProvider.GetRequiredService<Context>();
    await identity.Database.MigrateAsync();
    
    var serverMain = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    await serverMain.Database.MigrateAsync();

    var serverGrand = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
    await serverGrand.Database.MigrateAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

app.Run();
