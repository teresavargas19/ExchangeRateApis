using ExchangeRateComparer.Models;

namespace ExchangeRateComparer.Services;

public interface IExchangeRateService
{
    Task<ExchangeResult?> GetBestRateAsync();
}
