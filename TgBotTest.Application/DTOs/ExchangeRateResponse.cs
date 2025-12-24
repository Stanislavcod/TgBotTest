using System.Text.Json.Serialization;

namespace TgBotTest.Application;

public record ExchangeRateResponse(
    [property: JsonPropertyName("base_code")] string BaseCode,
    [property: JsonPropertyName("time_last_update_utc")] string Date,
    [property: JsonPropertyName("rates")] Dictionary<string, decimal> ConversionRates);
