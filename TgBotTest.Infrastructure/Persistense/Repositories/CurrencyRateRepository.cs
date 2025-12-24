using Microsoft.EntityFrameworkCore;
using TgBotTest.Application;
using TgBotTest.Domain;

namespace TgBotTest.Infrastructure.Persistense;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly AppDbContext _context;

    public CurrencyRateRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<CurrencyRate?> GetAsync(
        DateOnly date,
        string baseCurrency,
        string quoteCurrency,
        CancellationToken ct)
    {
        return _context.CurrencyRates.FirstOrDefaultAsync(
            x => x.RateDate == date &&
                 x.Base == baseCurrency &&
                 x.Quote == quoteCurrency,
            ct);
    }

    public async Task AddRangeAsync(IEnumerable<CurrencyRate> rates, CancellationToken ct) 
    { 
        await _context.CurrencyRates.AddRangeAsync(rates, ct); 
    }
}
