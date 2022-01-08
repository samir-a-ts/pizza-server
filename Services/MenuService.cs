namespace PizzaAPI.Services;

using PizzaAPI.Models;

public class MenuService {
    private readonly MongoDB.Driver.IMongoCollection<MenuItem> Collection;

    public MenuService(MongoDB.Driver.IMongoCollection<MenuItem> collection) {
        Collection = collection;
    }

    public async Task<List<MenuItem>> GetList() {
        var result = await Collection.FindAsync<MenuItem>(
            MongoDB.Driver.FilterDefinition<MenuItem>.Empty
        );
        
        return result.Current.ToList();
    }
}