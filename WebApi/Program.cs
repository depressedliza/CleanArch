using System.IdentityModel.Tokens.Jwt;
using Application;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddDbContext<SocialContext>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var authority = config.GetSection("Authentication:Authority").Get<string>();
var audience = config.GetSection("Authentication:Audience").Get<string>();
var requireScope = config.GetSection("Authentication:RequireScope").Get<string>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.Authority = authority;
        opt.Audience = audience;
        var validator = opt.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().SingleOrDefault();

        // Turn off Microsoft's JWT handler that maps claim types to .NET's long claim type names
        validator.InboundClaimTypeMap = new Dictionary<string, string>();
        validator.OutboundClaimTypeMap = new Dictionary<string, string>();
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(audience, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", requireScope);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SocialContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization(audience);

app.Run();
