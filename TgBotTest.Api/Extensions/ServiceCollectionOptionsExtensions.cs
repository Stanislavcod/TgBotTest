using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgBotTest.Application;

namespace TgBotTest.Api;

public static class ServiceCollectionOptionsExtensions
{
    public static IServiceCollection AddAppOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(
            configuration.GetSection("Telegram"));

        services.Configure<ExchangeRateApi>(
            configuration.GetSection("ExchangeRateApi"));

        return services;
    }
}
