using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotTest.Api;

public static class KeyboardFactory
{
    public static InlineKeyboardMarkup CurrencyKeyboard(IEnumerable<string> currencies)
    {
        var buttons = currencies
            .Select(c => InlineKeyboardButton.WithCallbackData(c, c))
            .ToArray();

        return new InlineKeyboardMarkup(buttons);
    }
}

