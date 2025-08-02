using ExchangeRateComparer.Models;
using System.Text.Json;

namespace ExchangeRateComparer.Strategies;

public class Api1Strategy : IApiStrategy
{
    private readonly HttpClient _client;
    public string Name => "Api1";

    public Api1Strategy(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task<ExchangeResult?> GetRateAsync()
    {
        try
        {
            var response = await _client.PostAsync("http://api1:8080/random1", null);
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var rate = doc.RootElement.GetProperty("rate").GetDecimal();
            return new ExchangeResult { SourceApi = Name, ConvertedAmount = rate };
        }
        catch { return null; }
    }
}
