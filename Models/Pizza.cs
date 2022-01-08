namespace PizzaAPI.Models;

public class Pizza : MenuItem {
    /// Dictionary, which returns price depending on
    /// pizza's size.
    public SortedDictionary<string, int> PriceDictionary {get; set;}
}