using Backend.API.Entities;
using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly GeneralService _generalService;

    public InvoicesController(
        GeneralService generalService
    )
    {
        _generalService = generalService;
    }

    [HttpPost]
    public async Task<IActionResult> Pay([FromBody] Invoice invoice)
    {
        var services = await _generalService.PayInvoice(invoice);

        return Ok(services);
    }
}

