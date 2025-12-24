using TgBotTest.Domain;

namespace TgBotTest.Application;

public interface ICurrencyRateService
{
    Task<CurrencyRate> GetRateAsync(
        string baseCurrency,
        string quoteCurrency,
        CancellationToken ct);
}
