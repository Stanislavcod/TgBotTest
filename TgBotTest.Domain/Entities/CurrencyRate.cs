namespace TgBotTest.Domain;
public class CurrencyRate
{
    public int Id { get; set; }
    public DateOnly RateDate { get; set; }
    public string Base { get; set; } = default!;
    public string Quote { get; set; } = default!;
    public decimal Rate { get; set; }
    public string Source { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
