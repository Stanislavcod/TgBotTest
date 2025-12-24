namespace TgBotTest.Application;

public interface ICurrencyApiClient
{
    Task<(Dictionary<string, decimal> Rates, DateOnly Date)> GetAllRatesAsync(string baseCurrency, CancellationToken ct);

}
