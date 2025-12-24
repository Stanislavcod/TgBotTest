using Microsoft.Extensions.DependencyInjection;
using TgBotTest.Application;

namespace TgBotTest.Api;

public static class ServiceCollectionApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICurrencyRateService, CurrencyRateService>();

        return services;
    }
}
