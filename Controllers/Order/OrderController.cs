namespace PizzaAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult order(
    ) {
        return Ok();
    }
}