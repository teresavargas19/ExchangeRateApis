namespace ExchangeRateComparer.Models;

public class ExchangeResult
{
    public string SourceApi { get; set; } = "";
    public decimal ConvertedAmount { get; set; }
}