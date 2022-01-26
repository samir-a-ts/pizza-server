namespace PizzaAPI.Controllers;

using PizzaAPI.Models;
public class GetPizzaModel : RequestResultBase {
    public Pizza? Pizza {get; set;}
}