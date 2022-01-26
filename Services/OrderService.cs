namespace PizzaAPI.Services;

using MongoDB.Driver;
using MongoDB.Bson;
using PizzaAPI.Models;

public class OrderService
{
    private readonly IMongoCollection<UserOrderCollection> UserOrderCollection;

    private readonly MenuService MenuService;

    public OrderService(IMongoCollection<UserOrderCollection> userOrderCollection, MenuService menuService)
    {
        UserOrderCollection = userOrderCollection;
        MenuService = menuService;
    }

    private async Task<UserOrderCollection> _GetCollection(Int32 id)
    {
        var collectionRequest = await UserOrderCollection.FindAsync<UserOrderCollection>(
            new BsonDocumentFilterDefinition<UserOrderCollection>(
                new BsonDocument(
                    "UserId", id
                )
            )
        );

        return collectionRequest.FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrders(Int32 id)
    {
        var collection = await _GetCollection(id);

        return collection.Orders ?? new List<Order>();
    }

    public async void CreateOrdersCollection(string id)
    {
        await UserOrderCollection.InsertOneAsync(
            new UserOrderCollection
            {
                UserId = id,
                Orders = new List<Order>()
            }
        );
    }

    public async Task<Order> CreateOrder(int id, OrderModel order)
    {
        var collection = await _GetCollection(id);

        var orders = collection.Orders as List<Order>;

        // var update = Builders<UserOrderCollection>.Update
        //     .Set(p => collection.Orders, orders.Add(order));

        // await UserOrderCollection.UpdateOneAsync(
        //     new BsonDocumentFilterDefinition<UserOrderCollection>(
        //         new  BsonDocument(
        //             "UserId", id
        //         )
        //     ),
        //     update
        // );


        return default;
    }
}