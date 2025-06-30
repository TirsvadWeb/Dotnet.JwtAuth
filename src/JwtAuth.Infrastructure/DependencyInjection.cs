using JwtAuth.Infrastructure.Data;
using JwtAuth.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtAuth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("AuthDatabase")));

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
