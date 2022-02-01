namespace PizzaAPI.Controllers;

using PizzaAPI.Models;

public class OrderHistoryResult : RequestResultBase {
    public IEnumerable<Order>? orders {get; set;}
}