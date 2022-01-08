namespace PizzaAPI.Config;

public class MongoDBOptions {
    public const string Position = "MongoDB";

    public string Url {get; set;} = String.Empty;

    public string DatabaseName {get; set;} = String.Empty;
}
