using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings));
        var connectionString = databaseSettings.GetSection(nameof(DatabaseSettings.ConnectionString)).Value;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, 
                opt => opt.UseNodaTime());
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<DatabaseInitializer>();
        return services;
    }
}