using Backend.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var services = new List<Service> {
            new() { Id = 1, Name = "Service 1", Price = (float)Math.Round(new Random().NextDouble() * 100, 2)},
            new() { Id = 2, Name = "Service 2", Price = (float)Math.Round(new Random().NextDouble() * 100, 2) }
        };

        return Ok(services);
    }
}

