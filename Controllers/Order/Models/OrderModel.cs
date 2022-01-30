namespace PizzaAPI.Models;

using System.Text.Json;

public class OrderModel {
    public JsonElement? MenuItemsId {get; set;}

    public string? Date { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Description { get; set; }
}