namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OrderItemModel {
    
    [BsonElement("ItemId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ItemId {get; set;}

    [BsonElement("ItemName")]
    [BsonRepresentation(BsonType.String)]
    public string? ItemName {get; set;}
}

public class PizzaItemModel : OrderItemModel {
    
    [BsonElement("PizzaSize")]
    [BsonRepresentation(BsonType.String)]
    public string? Size {get; set;}

    public PizzaItemModel() {
        ItemName = "pizza";
    }
}

public class ComboItemModel : OrderItemModel {
    public ComboItemModel() {
        ItemName = "combo";
    }
}