using ExchangeRateComparer.Models;
using System.Text.Json;

namespace ExchangeRateComparer.Strategies;

public class Api3Strategy : IApiStrategy
{
    private readonly HttpClient _client;
    public string Name => "Api3";

    public Api3Strategy(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task<ExchangeResult?> GetRateAsync()
    {
        try
        {
            var response = await _client.PostAsync("http://api3:8080/random3", null);
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var total = doc.RootElement.GetProperty("data").GetProperty("total").GetDecimal();
            return new ExchangeResult { SourceApi = Name, ConvertedAmount = total };
        }
        catch { return null; }
    }
}
