namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class OrderDocument
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? ObjectId { get; set; }

    [BsonRepresentation(BsonType.String)]
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
    public IEnumerable<BsonDocument>? MenuItemsId {get; set;}
}

