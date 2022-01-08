namespace PizzaAPI.Services;

using PizzaAPI.Models;

public class MenuService {
    private readonly MongoDB.Driver.IMongoCollection<MenuItem> Database;

    public MenuService(MongoDB.Driver.IMongoCollection<MenuItem> database) {
        Database = database;
    }
}