namespace PizzaAPI.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User {
    [BsonRepresentation(BsonType.String)]
    [BsonElement("Username")]
    public string Username {get; set;}

    [BsonElement("UserId")]
    [BsonRepresentation(BsonType.Int64)]
    public Int64 Id {get; set;}

    [BsonRepresentation(BsonType.String)]
    [BsonElement("Email")]
    public string Email {get; set;}

    [BsonRepresentation(BsonType.String)]
    [BsonElement("Password")]
    public string Password {get; set;}

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string ObjectId { get; set; }
}