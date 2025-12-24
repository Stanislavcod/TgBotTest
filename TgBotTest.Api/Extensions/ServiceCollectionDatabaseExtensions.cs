using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgBotTest.Application;
using TgBotTest.Infrastructure;
using TgBotTest.Infrastructure.Persistense;

namespace TgBotTest.Api;

public static class ServiceCollectionDatabaseExtensions
{
    public static IServiceCollection AddDatabase(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default")));

        services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
