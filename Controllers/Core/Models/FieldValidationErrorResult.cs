namespace PizzaAPI.Controllers;

public class FieldValidationErrorResult : RequestResultBase {
    public string? ErrorMessage {get; set;}

    public string? FieldName {get; set;}
}