using Backend.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var customers = new List<Customer> {
            new() { Id = 1, Name = "John Doe" },
            new() { Id = 2, Name = "Dann Brown" }
        };

        return Ok(customers);
    }

}

