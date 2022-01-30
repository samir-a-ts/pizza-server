namespace PizzaAPI.Config;

public class JwtOptions {
    public const string Position = "Jwt";

    public string Issuer {get; set;} = String.Empty;
    

    public string Audience {get; set;} = String.Empty;
    
    public string AudienceRefresh {get; set;} = String.Empty;

}
