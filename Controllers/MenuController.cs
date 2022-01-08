namespace PizzaAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase {

    private readonly ILogger<MenuController> _logger;

    public MenuController(ILogger<MenuController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "check")]
    public IActionResult GetCheck([FromServices] MenuService service) {
        if (service == null) return NoContent();

        return Ok();
    }

}