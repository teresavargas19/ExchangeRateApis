using ExchangeRateComparer.Models;
using System.Xml.Linq;
namespace ExchangeRateComparer.Strategies;

public class Api2Strategy : IApiStrategy
{
    private readonly HttpClient _client;
    public string Name => "Api2";

    public Api2Strategy(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
    }

    public async Task<ExchangeResult?> GetRateAsync()
    {
        try
        {
            var content = new StringContent("<XML></XML>", System.Text.Encoding.UTF8, "application/xml");
            var response = await _client.PostAsync("http://api2:8080/random2", content);
            var xml = XDocument.Parse(await response.Content.ReadAsStringAsync());
            var result = xml.Root?.Element("Result")?.Value;
            return new ExchangeResult { SourceApi = Name, ConvertedAmount = decimal.Parse(result ?? "0") };
        }
        catch { return null; }
    }
}
