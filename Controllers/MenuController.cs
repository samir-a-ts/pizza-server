using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;

using PizzaAPI.Models;
namespace PizzaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase {
    
    [HttpGet]
    async Task<List<MenuItem>> Get([FromServices] MenuService service) {
        var result = await service.GetList();

        return result;
    }

}