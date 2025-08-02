using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    
    [ApiController]
    [Route("Random1")] 
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetRandom()
        {
            double rate = new Random().NextDouble() * 100;
            return Ok(new { rate = Math.Round(rate, 2) });
        }
    }
}
