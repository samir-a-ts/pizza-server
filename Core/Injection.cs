namespace PizzaAPI.Injection;

using PizzaAPI.Config;
using PizzaAPI.Models;
using PizzaAPI.Services;

public class Injection {

    public static void Init(IServiceCollection Collection, IConfiguration Configuration) {
        var mongoDBOptions = new MongoDBOptions();

        ConfigurationBinder.Bind(Configuration, MongoDBOptions.Position, mongoDBOptions);

        Console.WriteLine(mongoDBOptions);

        var builder = new MongoDB.Driver.MongoUrlBuilder(mongoDBOptions.Url);

        var client = new MongoDB.Driver.MongoClient(builder.ToMongoUrl());

        var database = client.GetDatabase(mongoDBOptions.DatabaseName);

        var menuCollection = database.GetCollection<MenuItem>("menu");

        Collection.AddSingleton<MenuService>(
            new MenuService(menuCollection)
        );
    }
}