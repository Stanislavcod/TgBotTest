using Microsoft.EntityFrameworkCore;
using TgBotTest.Domain;

namespace TgBotTest.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyRate>()
            .HasIndex(x => new { x.RateDate, x.Base, x.Quote })
            .IsUnique();
    }
}

