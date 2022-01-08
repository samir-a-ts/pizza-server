namespace PizzaAPI.Services;

using MongoDB.Driver;

using PizzaAPI.Models;

public class MenuService
{
    private readonly IMongoCollection<Pizza> PizzaCollection;

    private readonly IMongoCollection<Combo> ComboCollection;

    public MenuService(IMongoCollection<Pizza> pizzaCollection, IMongoCollection<Combo> comboCollection)
    {
        PizzaCollection = pizzaCollection;
        ComboCollection = comboCollection;
    }

    public async Task<IEnumerable<Combo>> GetCombosList() {
        var result = new List<Combo>();

        var combos = await ComboCollection.FindAsync<Combo>(
            FilterDefinition<Combo>.Empty
        );

        var combosList = combos.ToList();

        Console.WriteLine(combosList);

        result = result.Concat(combosList ?? new List<Combo>()).ToList();

        return result;
    }

    public async Task<IEnumerable<Pizza>> GetPizzaList()
    {
        var result = new List<Pizza>();

        var pizzas = await PizzaCollection.FindAsync<Pizza>(
            FilterDefinition<Pizza>.Empty
        );

        var pizzaList = pizzas.ToList();

        Console.WriteLine(pizzaList);

        result = result.Concat(pizzaList ?? new List<Pizza>()).ToList();

        return result;
    }

    public async Task<Pizza?> GetPizzaFromId(int id)
    {
        var pizzaFilter = await PizzaCollection.FindAsync<Pizza>(
            new BsonDocumentFilterDefinition<Pizza>(
                new MongoDB.Bson.BsonDocument(
                    "ItemId", id
                )
            )
        );

        return pizzaFilter.FirstOrDefault();
    }

    public async Task<Combo?> GetComboFromId(int id)
    {
        var comboFilter = await ComboCollection.FindAsync<Combo>(
            new BsonDocumentFilterDefinition<Combo>(
                new MongoDB.Bson.BsonDocument(
                    "ItemId", id
                )
            )
        );


        return comboFilter.FirstOrDefault();
    }
}