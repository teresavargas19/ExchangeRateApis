using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api3.Controllers
{
    
    [ApiController]
    [Route("Random3")] 
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetRandom()
            {
                double total = new Random().NextDouble() * 100;
                return Ok(new
                {
                 statusCode = 200,
                 message = "ok",
                 data = new { total = Math.Round(total, 2) }
                });
            }
        }
}