namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MenuItem
{
    /// Current name of that pizza.
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? ObjectId { get; set; }


    [BsonRepresentation(BsonType.String)]
    [BsonElement("Name")]
    public string? Name { get; set; }

    /// URL for pizza image.
    [BsonRepresentation(BsonType.String)]
    [BsonElement("ImageUrl")]
    public string? ImageUrl { get; set; }

    /// Pizza description.
    [BsonRepresentation(BsonType.String)]
    [BsonElement("Description")]
    public string? Description {get; set;}
}