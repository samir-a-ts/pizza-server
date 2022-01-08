namespace PizzaAPI.Models;

public class Combo : MenuItem { 
    /// What this combo actually includes.
    public List<string> Inclusions {get; set;}

    /// Price of this combo.
    public int Price {get; set;}
}