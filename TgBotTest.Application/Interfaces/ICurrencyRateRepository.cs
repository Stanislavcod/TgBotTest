using TgBotTest.Domain;

namespace TgBotTest.Application;

public interface ICurrencyRateRepository
{
    Task<CurrencyRate?> GetAsync(
        DateOnly date,
        string baseCurrency,
        string quoteCurrency,
        CancellationToken ct);

    Task AddRangeAsync(IEnumerable<CurrencyRate> rates, CancellationToken ct);
}
