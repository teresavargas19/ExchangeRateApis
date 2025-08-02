using ExchangeRateComparer.Models;

namespace ExchangeRateComparer.Strategies;

public interface IApiStrategy
{
    Task<ExchangeResult?> GetRateAsync();
    string Name { get; }
}