using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TgBotTest.Application;

namespace TgBotTest.Api;

public static class ServiceCollectionTelegramExtensions
{
    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services)
    {
        services.AddSingleton<ITelegramBotClient>(sp =>
        {
            var options = sp
                .GetRequiredService<IOptions<TelegramOptions>>()
                .Value;

            return new TelegramBotClient(options.Token);
        });

        services.AddScoped<IUpdateHandler, TelegramUpdateHandler>();

        return services;
    }
}
