using System.Text.Json;
using System.Globalization;
using Microsoft.Extensions.Logging;
using TgBotTest.Application;

namespace TgBotTest.Infrastructure;

public class ExchangeRateClient : ICurrencyApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<ExchangeRateClient> _logger;

    public ExchangeRateClient(HttpClient http, ILogger<ExchangeRateClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<(Dictionary<string, decimal> Rates, DateOnly Date)> GetAllRatesAsync(string baseCurrency, CancellationToken ct)
    {
        var raw = await _http.GetStringAsync($"latest/{baseCurrency}", ct);
        _logger.LogInformation("ExchangeRate API raw response: {Raw}", raw);

        var response = JsonSerializer.Deserialize<ExchangeRateResponse>(raw)
            ?? throw new InvalidOperationException("Empty response from API");

        if (response.ConversionRates is null)
            throw new InvalidOperationException("ConversionRates is null in API response");

        var dt = DateTime.Parse(response.Date, CultureInfo.InvariantCulture);
        var date = DateOnly.FromDateTime(dt);

        return (response.ConversionRates, date);
    }
}
