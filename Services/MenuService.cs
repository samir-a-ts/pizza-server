namespace PizzaAPI.Services;

using MongoDB.Driver;
using MongoDB.Bson;

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

        result = result.Concat(pizzaList ?? new List<Pizza>()).ToList();

        return result;
    }

    public async Task<Pizza?> GetPizzaFromId(string id)
    {
        var pizzaFilter = await PizzaCollection.FindAsync<Pizza>(
            new BsonDocumentFilterDefinition<Pizza>(
                new BsonDocument(
                     "_id", new ObjectId(id)
                )
            )
        );

        if (!pizzaFilter.Any()) return null;

        return pizzaFilter.FirstOrDefault();
    }

    public async Task<Combo?> GetComboFromId(string id)
    {
        var comboFilter = await ComboCollection.FindAsync<Combo>(
            new BsonDocumentFilterDefinition<Combo>(
                new BsonDocument(
                    "_id", new ObjectId(id)
                )
            )
        );


        if (!comboFilter.Any()) return null;

        return comboFilter.FirstOrDefault();
    }

    public async Task<IEnumerable<Pizza>> GetPizzasFromIds(IEnumerable<string> ids)
    {
        var filters = new List<BsonElement>();

        foreach (var id in ids)
        {
            filters.Add(
                new BsonElement(
                    "_id", id
                )
            );
        }

        var pizzaFilter = await PizzaCollection.FindAsync<Pizza>(
            new BsonDocumentFilterDefinition<Pizza>(
                new BsonDocument(filters)
            )
        );

        return pizzaFilter.ToList();
    }

        public async Task<IEnumerable<Combo>> GetCombosFromIds(IEnumerable<Int32> ids)
    {
        var filters = new List<BsonElement>();

        foreach (var id in ids)
        {
            filters.Add(
                new BsonElement(
                    "_id", id
                )
            );
        }


        var comboFilter = await ComboCollection.FindAsync<Combo>(
            new BsonDocumentFilterDefinition<Combo>(
                new BsonDocument(filters)
            )
        );


        return comboFilter.ToList();
    }
}