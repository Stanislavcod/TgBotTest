using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotTest.Application;
using Microsoft.Extensions.Logging;

namespace TgBotTest.Api;

public class TelegramUpdateHandler : IUpdateHandler
{
    private readonly ICurrencyRateService _rateService;
    private readonly ILogger<TelegramUpdateHandler> _logger;

    private static readonly string[] SupportedCurrencies =
    {
        "USD", "EUR", "CNY", "KZT", "TRY", "AED", "GBP", "JPY"
    };

    public TelegramUpdateHandler(ICurrencyRateService rateService, ILogger<TelegramUpdateHandler> logger)
    {
        _rateService = rateService;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        if (update.Type == UpdateType.Message && update.Message?.Text == "/start")
        {
            await SendCurrencyKeyboard(bot, update.Message.Chat.Id, ct);
            return;
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var data = update.CallbackQuery!.Data!;

            if (data == "exit")
            {
                await bot.SendMessage(
                    chatId: update.CallbackQuery.Message!.Chat.Id,
                    text: "Спасибо за использование бота! 👋",
                    cancellationToken: ct
                );
                return;
            }

            var rate = await _rateService.GetRateAsync("RUB", data, ct);

            await bot.SendMessage(
                chatId: update.CallbackQuery.Message!.Chat.Id,
                text: $"Курс RUB → {data}\n{rate.Rate}\nДата: {rate.RateDate:yyyy-MM-dd}",
                cancellationToken: ct
            );

            await SendCurrencyKeyboard(bot, update.CallbackQuery.Message.Chat.Id, ct);
        }
    }

    private async Task SendCurrencyKeyboard(ITelegramBotClient bot, long chatId, CancellationToken ct)
    {
        var currencyButtons = SupportedCurrencies
            .Select(c => InlineKeyboardButton.WithCallbackData(c, c))
            .ToArray();

        var exitButton = InlineKeyboardButton.WithCallbackData("Закончить", "exit");

        var keyboard = new InlineKeyboardMarkup(new[]
        {
        currencyButtons,              
        new[] { exitButton }       
    });

        await bot.SendMessage(
            chatId: chatId,
            text: "Выберите валюту:",
            replyMarkup: keyboard,
            cancellationToken: ct
        );
    }


    public Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, HandleErrorSource errorSource, CancellationToken ct)
    {
        _logger.LogError(exception, "Telegram bot error ({Source})", errorSource);
        return Task.CompletedTask;
    }
}
