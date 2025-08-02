using Microsoft.AspNetCore.Mvc;
using Api1.Models;

namespace Api1.Controllers
{
    [ApiController]
    [Route("Random1")]
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetRandom([FromBody] ExchangeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.From) || string.IsNullOrWhiteSpace(request.To) || request.Value <= 0)
            {
                return BadRequest(new { message = "Campos inválidos: from, to y value son requeridos y deben ser válidos." });
            }

            // Simulación de una tasa
            double rate = new Random().NextDouble() * 100;
            double converted = rate * (double)request.Value;

            return Ok(new
            {
                rate = Math.Round(converted, 2)
            });
        }
    }
}
