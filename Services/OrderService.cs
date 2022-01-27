namespace PizzaAPI.Services;

using MongoDB.Driver;
using MongoDB.Bson;
using PizzaAPI.Models;
using System.Text.Json;

using MongoDB.Bson.Serialization.IdGenerators;

public class OrderService
{
    private readonly IMongoCollection<UserOrderCollection> UserOrderCollection;

    private readonly MenuService MenuService;

    public OrderService(IMongoCollection<UserOrderCollection> userOrderCollection, MenuService menuService)
    {
        UserOrderCollection = userOrderCollection;
        MenuService = menuService;
    }

    private async Task<UserOrderCollection> _GetCollection(string id)
    {
        var collectionRequest = await UserOrderCollection.FindAsync<UserOrderCollection>(
            new BsonDocumentFilterDefinition<UserOrderCollection>(
                new BsonDocument(
                    "UserId", new ObjectId(id)
                )
            )
        );

        return collectionRequest.FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrders(string id)
    {
        var collection = await _GetCollection(id);

        return collection.Orders ?? new List<Order>();
    }

    public async void CreateOrdersCollection(string id)
    {
        var collection = new UserOrderCollection
            {
                UserId = id,
                Orders = new List<Order>(),

            };

        collection.ObjectId = ObjectIdGenerator.Instance.GenerateId("orders", collection).ToString()!;

        await UserOrderCollection.InsertOneAsync(
            collection
        );
    }

    public async Task<Order> CreateOrder(string id, OrderModel model) 
    {
        var collection = await _GetCollection(id);

        var orders = collection.Orders as List<Order>;

        var menuItems = new List<OrderItemModel>();

        var list = new List<BsonDocument>();

        foreach (var item in model.MenuItemsId!.Value.EnumerateArray())
        {
            var document = new BsonDocument();

            foreach (var key in item.EnumerateObject())
            {
                var property = item.GetProperty(key.Name);

                document[key.Name] = new BsonString(property.ToString());
            }

            Console.WriteLine(document);

            list.Add(document);
        }

        var order = new Order {
            Address = model.Address,
            Date = model.Date!,
            Time = model.Time!,
            Description = model.Description,
            MenuItemsId = list,
            PhoneNumber = model.PhoneNumber
        };

        order.ObjectId = ObjectIdGenerator.Instance.GenerateId("orders", order).ToString()!;

        orders!.Add(order);

        await UserOrderCollection.ReplaceOneAsync(
            new BsonDocumentFilterDefinition<UserOrderCollection>(
                new  BsonDocument(
                    "UserId", new ObjectId(id)
                )
            ),
            new UserOrderCollection {
                ObjectId = collection.ObjectId,
                Orders = orders,
                UserId = collection.UserId,
            }
        );

        return order;
    }
}