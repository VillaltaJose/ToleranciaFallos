using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly GeneralService _generalService;

    public ServicesController(
        GeneralService generalService
    )
    {
        _generalService = generalService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int customerId) {
        var services = await _generalService.GetServices(customerId);

        return Ok(services);
    }
}

