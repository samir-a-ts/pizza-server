using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;

namespace PizzaAPI.Controllers;

[ApiController]
[Route("menu")]
public class MenuController : ControllerBase {

    public MenuController() {}
    
    [HttpGet]
    public async Task<GetMenuModel> Get([FromServices] MenuService service) {
        var pizzasRequest = service.GetPizzaList();
        var combosRequest = service.GetCombosList();

        var pizzas = await pizzasRequest;
        var combos = await combosRequest;

        return new GetMenuModel {
            Pizzas = pizzas,
            Combos = combos,
        };
    }

}