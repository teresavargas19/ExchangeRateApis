using ExchangeRateComparer.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ExchangeRateComparer.Strategies
{
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
                // Crear el JSON de entrada
                var payload = new
                {
                    from = "USD",
                    to = "DOP",
                    value = 100
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("http://api1:8080/random1", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Api1] Error: Código HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("[Api1] Error: respuesta vacía del servicio.");
                    return null;
                }

                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("rate", out var rateElement))
                {
                    Console.WriteLine("[Api1] Error: 'rate' no encontrado en la respuesta.");
                    return null;
                }

                if (!rateElement.TryGetDecimal(out var rate))
                {
                    Console.WriteLine("[Api1] Error: 'rate' no es un número decimal válido.");
                    return null;
                }

                return new ExchangeResult
                {
                    SourceApi = Name,
                    ConvertedAmount = rate
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[Api1] Error de red: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Api1] Error al leer JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api1] Error inesperado: {ex.Message}");
                return null;
            }
        }
    }
}
