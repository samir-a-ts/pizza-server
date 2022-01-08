using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;

[ApiController]
[Route("[controller]")]
public class MenuController {
    
    [HttpGet("check")]
    bool check([FromServices] MenuService service) {
        return service == null;
    }

}