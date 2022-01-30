
namespace PizzaAPI.Controllers;

using PizzaAPI.Models;

public class GetMenuModel : RequestResultBase {
    public IEnumerable<Pizza>? Pizzas {get; set;}

    public IEnumerable<Combo>? Combos {get; set;}
}