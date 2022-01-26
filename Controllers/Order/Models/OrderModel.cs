namespace PizzaAPI.Models;

public class OrderModel {
    public IEnumerable<OrderItemModel>? MenuItemsId {get; set;}

    public string? Date { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Description { get; set; }
}