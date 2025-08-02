using ExchangeRateComparer.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ExchangeRateComparer.Strategies
{
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
                // ✅ Aquí están los nombres correctos que espera el modelo ExchangeRequest
                var requestBody = new
                {
                    From = "USD",
                    To = "DOP",
                    Value = 100
                };

                var jsonRequest = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _client.PostAsync("http://api3:8080/random3", jsonRequest);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Api3] Error: Código HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("[Api3] Error: respuesta vacía del servicio.");
                    return null;
                }

                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("data", out var dataElement))
                {
                    Console.WriteLine("[Api3] Error: 'data' no encontrado en la respuesta.");
                    return null;
                }

                if (!dataElement.TryGetProperty("total", out var totalElement))
                {
                    Console.WriteLine("[Api3] Error: 'total' no encontrado dentro de 'data'.");
                    return null;
                }

                if (!totalElement.TryGetDecimal(out var total))
                {
                    Console.WriteLine("[Api3] Error: 'total' no es un número decimal válido.");
                    return null;
                }

                return new ExchangeResult
                {
                    SourceApi = Name,
                    ConvertedAmount = total
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[Api3] Error de red: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Api3] Error al leer JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api3] Error inesperado: {ex.Message}");
                return null;
            }
        }
    }
}
