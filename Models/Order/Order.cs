namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Order
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? ObjectId { get; set; }

    [BsonElement("OrderId")]
    [BsonRepresentation(BsonType.Int32)]
    public Int32? OrderId { get; set; }

    [BsonRepresentation(BsonType.Timestamp)]
    [BsonElement("Date")]
    public string? Date { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonElement("Address")]
    public string? Address { get; set; }

    [BsonElement("Phone_Number")]
    [BsonRepresentation(BsonType.String)]
    public string? PhoneNumber { get; set; }

    [BsonElement("Commentaries")]
    [BsonRepresentation(BsonType.String)]
    public string? Description { get; set; }

    [BsonElement("Items")]
    [BsonRepresentation(BsonType.Array)]
    public IEnumerable<OrderItemModel>? MenuItemsId {get; set;}

    public Int16 Price {get; set;}
}