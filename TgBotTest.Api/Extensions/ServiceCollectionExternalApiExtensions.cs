using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TgBotTest.Application;
using TgBotTest.Infrastructure;

namespace TgBotTest.Api;

public static class ServiceCollectionExternalApiExtensions
{
    public static IServiceCollection AddExternalApis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<ICurrencyApiClient, ExchangeRateClient>(
            (sp, client) =>
            {
                var options = sp
                    .GetRequiredService<IOptions<ExchangeRateApi>>()
                    .Value;

                client.BaseAddress = new Uri(options.BaseUrl);
            });

        return services;
    }
}
