using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Api2.Models;

namespace Api2.Controllers
{
    [Route("Random2")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/xml")]
        public IActionResult GetRandom([FromBody] ExchangeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.From) || string.IsNullOrWhiteSpace(request.To) || request.Value <= 0)
            {
                var errorXml = new XElement("Error", "Campos inválidos: from, to y value son requeridos.");
                return Content(errorXml.ToString(), "application/xml");
            }

            double rate = new Random().NextDouble() * 100;
            double converted = rate * (double)request.Value;

            var response = new XElement("XML", new XElement("Result", Math.Round(converted, 2)));
            return Content(response.ToString(), "application/xml");
        }
    }
}
