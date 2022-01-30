namespace PizzaAPI.Controllers;

using PizzaAPI.Models;

public class OrderSuccessModel : RequestResultBase {
    public Order? Order {get; set;}
}