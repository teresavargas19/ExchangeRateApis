using Microsoft.AspNetCore.Mvc;
using ExchangeRateComparer.Services;

namespace ExchangeRateComparer.Controllers;

[ApiController]
[Route("compare")]
public class CompareController : ControllerBase
{
    private readonly IExchangeRateService _service;

    public CompareController(IExchangeRateService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetBestRateAsync();
        return result == null ? NotFound("No APIs responded correctly.") : Ok(result);
    }
}
