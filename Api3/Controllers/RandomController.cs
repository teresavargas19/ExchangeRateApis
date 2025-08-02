using Microsoft.AspNetCore.Mvc;
using Api3.Models;

namespace Api3.Controllers
{
    [ApiController]
    [Route("Random3")]
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetRandom([FromBody] ExchangeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.From) || string.IsNullOrWhiteSpace(request.To) || request.Value <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Campos inválidos: from, to y value son requeridos y value debe ser mayor a 0.",
                    data = new { total = 0 }
                });
            }

            double rate = new Random().NextDouble() * 100;
            double converted = rate * (double)request.Value;

            return Ok(new
            {
                statusCode = 200,
                message = "ok",
                data = new { total = Math.Round(converted, 2) }
            });
        }
    }
}
