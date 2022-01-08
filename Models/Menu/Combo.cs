namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Combo : MenuItem { 
    /// What this combo actually includes.
    [BsonElement("Inclusions")]
    public List<string> Inclusions {get; set;}

    /// Price of this combo.
    [BsonElement("Price")]
    [BsonRepresentation(BsonType.Int32)]
    public int Price {get; set;}
}