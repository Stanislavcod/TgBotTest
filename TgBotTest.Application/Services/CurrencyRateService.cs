using TgBotTest.Domain;

namespace TgBotTest.Application;

public class CurrencyRateService : ICurrencyRateService
{
    private readonly ICurrencyRateRepository _repo;
    private readonly ICurrencyApiClient _api;
    private readonly IUnitOfWork _uow;

    public CurrencyRateService(
        ICurrencyRateRepository repo,
        ICurrencyApiClient api,
        IUnitOfWork uow)
    {
        _repo = repo;
        _api = api;
        _uow = uow;
    }

    public async Task<CurrencyRate> GetRateAsync(
    string baseCurrency,
    string quoteCurrency,
    CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var cached = await _repo.GetAsync(today, baseCurrency, quoteCurrency, ct);
        if (cached != null)
            return cached;

        var (rates, date) = await _api.GetAllRatesAsync(baseCurrency, ct);

        var entities = rates.Select(pair => new CurrencyRate
        {
            RateDate = date,
            Base = baseCurrency,
            Quote = pair.Key,
            Rate = pair.Value,
            Source = "ExchangeRate-API",
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _repo.AddRangeAsync(entities, ct);
        await _uow.SaveChangesAsync(ct);

        return entities.First(x => x.Quote == quoteCurrency);
    }
}
