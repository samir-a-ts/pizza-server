namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Pizza : MenuItem {
    /// Dictionary, which returns price depending on
    /// pizza's size.
    [BsonElement("PriceDictionary")]
    [BsonRepresentation(BsonType.Document)]
    public SortedDictionary<string, int> PriceDictionary {get; set;}
}