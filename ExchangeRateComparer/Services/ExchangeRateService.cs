using ExchangeRateComparer.Models;
using ExchangeRateComparer.Strategies;

namespace ExchangeRateComparer.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IEnumerable<IApiStrategy> _strategies;

    public ExchangeRateService(IEnumerable<IApiStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task<ExchangeResult?> GetBestRateAsync()
    {
        var tasks = _strategies.Select(s => s.GetRateAsync());
        var results = await Task.WhenAll(tasks);
        return results.Where(r => r != null).OrderByDescending(r => r!.ConvertedAmount).FirstOrDefault();
    }
}
