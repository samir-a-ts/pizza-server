namespace PizzaAPI.Services;

using PizzaAPI.Models;

public class MenuService {
    private readonly MongoDB.Driver.IMongoCollection<Pizza> PizzaCollection;
    
    private readonly MongoDB.Driver.IMongoCollection<Combo> ComboCollection;

    public MenuService(MongoDB.Driver.IMongoCollection<Pizza> pizzaCollection, MongoDB.Driver.IMongoCollection<Combo> comboCollection) {
        PizzaCollection = pizzaCollection;
        ComboCollection = comboCollection;
    }

    public async Task<IEnumerable<MenuItem>> GetList() {
        var result = new List<MenuItem>();

        var pizzasRequest = PizzaCollection.FindAsync<Pizza>(
            MongoDB.Driver.FilterDefinition<Pizza>.Empty
        );

        var comboRequest = ComboCollection.FindAsync<Combo>(
            MongoDB.Driver.FilterDefinition<Combo>.Empty
        );

        var pizzas = await pizzasRequest;

        var combos = await comboRequest;

        result = result.Concat(
            pizzas.Current ?? new List<Pizza>()
            ).Concat(
                combos.Current ?? new List<Combo>()
        ).ToList();
        
        return result;
    }
}