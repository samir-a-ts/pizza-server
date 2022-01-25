namespace PizzaAPI.Models;

public class OrderModel {
    public IEnumerable<Int32> MenuItemsId {get; set;}

    public Int32? ComboId {get; set;}

    public string Date { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }
}