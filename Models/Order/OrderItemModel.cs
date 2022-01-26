namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OrderItemModel {

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? ObjectId { get; set; }
    
    [BsonElement("ItemId")]
    [BsonRepresentation(BsonType.Int32)]
    public Int32 ItemId {get; set;}

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