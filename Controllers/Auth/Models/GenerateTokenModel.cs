namespace PizzaAPI.Controllers;

public class GenerateTokenModel : RequestResultBase {
    public string? Token {get; set;}

    public string? RefreshToken {get; set;}
}