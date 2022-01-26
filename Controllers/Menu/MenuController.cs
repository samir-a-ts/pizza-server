using Microsoft.AspNetCore.Mvc;

using PizzaAPI.Services;
namespace PizzaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase {

    [HttpGet]
    public async Task<GetMenuModel> Get([FromServices] MenuService service) {
        var pizzasRequest = service.GetPizzaList();
        var combosRequest = service.GetCombosList();

        var pizzas = await pizzasRequest;
        var combos = await combosRequest;

        return new GetMenuModel {
            Pizzas = pizzas,
            Combos = combos,
            Result = "success"
        };
    }

    [HttpGet("pizza/{id}")]
    public async Task<RequestResultBase> GetPizza([FromServices] MenuService service, string id) {
        var pizzasRequest = await service.GetPizzaFromId(id);

        if (pizzasRequest == null) return new ErrorResult {
            ErrorMessage = "Not found pizza with this ID!",
            Result = "error",
            Code = 400
        };

        return new GetPizzaModel {
            Pizza = pizzasRequest,
            Result = "success",
            Code = 200
        };
    }

    [HttpGet("combo/{id}")]
    public async Task<RequestResultBase> GetCombo([FromServices] MenuService service, string id) {
        var comboRequest = await service.GetComboFromId(id);

        if (comboRequest == null) return new ErrorResult {
            ErrorMessage = "Not found pizza with this ID!",
            Result = "error",
            Code = 400
        };

        return new GetComboModel {
            Combo = comboRequest,
            Result = "success",
            Code = 200
        };
    }

}