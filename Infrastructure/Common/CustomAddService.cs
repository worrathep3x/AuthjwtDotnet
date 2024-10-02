using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Infrastructure.Common;

public static class CustomAddService
{
    public static void AddRepoService(this IServiceCollection services)
    {
        services.AddTransient<IUserAuthenRespository, UserAuthenRespository>();
    }
    public static void AddDbContextService(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddDbContext<EmployeeDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DatabaseConfig")));
        services.AddTransient<IEmployeeDbContext>(provider => provider.GetRequiredService<EmployeeDbContext>());
    }
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.SaveToken = true;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidIssuer = configuration["JwtConfig:Issuer"],
                 ValidateAudience = true,
                 ValidAudience = configuration["JwtConfig:Audience"],
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:ApiSecret"])),
                 ClockSkew = TimeSpan.Zero
             };
         });
    }
}
