namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UserOrderCollection
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? ObjectId { get; set; }

    [BsonElement("UserId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    [BsonElement("Orders")]
    public IEnumerable<OrderDocument>? Orders { get; set; }
}