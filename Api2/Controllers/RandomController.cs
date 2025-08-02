using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Api2.Controllers
{
   [Route("Random2")] 
    [ApiController]
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public ContentResult GetRandom()
        {
            double rate = new Random().NextDouble() * 100;
            var response = new XElement("XML", new XElement("Result", Math.Round(rate, 2)));
            return Content(response.ToString(), "application/xml");
        }
    }
}
