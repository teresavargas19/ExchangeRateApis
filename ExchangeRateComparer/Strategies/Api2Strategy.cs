using ExchangeRateComparer.Models;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace ExchangeRateComparer.Strategies
{
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
                // Construir XML de entrada con estructura esperada
                var requestXml = new XElement("ExchangeRequest",
                    new XElement("From", "USD"),
                    new XElement("To", "DOP"),
                    new XElement("Value", "100")
                );

                var content = new StringContent(requestXml.ToString(), Encoding.UTF8, "application/xml");

                var response = await _client.PostAsync("http://api2:8080/random2", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Api2] Error: Código HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }

                var xmlString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(xmlString))
                {
                    Console.WriteLine("[Api2] Error: respuesta vacía.");
                    return null;
                }

                var xml = XDocument.Parse(xmlString);

                var resultElement = xml.Root?.Element("Result");
                if (resultElement == null)
                {
                    Console.WriteLine("[Api2] Error: Elemento <Result> no encontrado en XML.");
                    return null;
                }

                if (!decimal.TryParse(resultElement.Value, out var rate))
                {
                    Console.WriteLine("[Api2] Error: El valor de <Result> no es un número decimal.");
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
                Console.WriteLine($"[Api2] Error de red: {ex.Message}");
                return null;
            }
            catch (System.Xml.XmlException ex)
            {
                Console.WriteLine($"[Api2] Error al parsear XML: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api2] Error inesperado: {ex.Message}");
                return null;
            }
        }
    }
}
