using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;

using PizzaAPI.Models;
namespace PizzaAPI.Controllers;

[ApiController]
[Route("menu")]
public class MenuController : ControllerBase {

    public MenuController() {}
    
    [HttpGet]
    public async Task<IEnumerable<MenuItem>> Get([FromServices] MenuService service) {
        var result = await service.GetList();

        return result;
    }

}