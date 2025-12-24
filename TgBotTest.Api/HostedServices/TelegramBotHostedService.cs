using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace TgBotTest.Api;

public class TelegramBotHostedService : BackgroundService
{
    private readonly ITelegramBotClient _bot;
    private readonly IUpdateHandler _updateHandler;

    public TelegramBotHostedService(
        ITelegramBotClient bot,
        IUpdateHandler updateHandler)
    {
        _bot = bot;
        _updateHandler = updateHandler;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _bot.StartReceiving(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken
        );

        return Task.CompletedTask;
    }
}
