using Backend.API.Entities;
using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly GeneralService _generalService;

    public CustomersController(
        GeneralService generalService
    )
    {
        _generalService = generalService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var customers = await _generalService.GetCustomers();

        return Ok(customers);
    }

}

