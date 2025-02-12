using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new Exception("Este es un error de prueba.");
    }
}