namespace PizzaAPI.Services;

using MongoDB.Driver;
using MongoDB.Bson;

using PizzaAPI.Models;
using MongoDB.Bson.Serialization.IdGenerators;


public class AuthService {
     private readonly IMongoCollection<User> UserCollection;

     public AuthService(IMongoCollection<User> userCollection) {
         UserCollection = userCollection;
     }

     public async Task<User?> findOne(Int16 id) {
         var userFilter = await UserCollection.FindAsync<User>(
            new BsonDocumentFilterDefinition<User>(
                new MongoDB.Bson.BsonDocument(
                    "UserId", id
                )
            )
        );

        return userFilter.FirstOrDefault();
     }

     public async Task<User?> findOne(string email, string password) {
        var userFilter = await UserCollection.FindAsync<User>(
            new BsonDocumentFilterDefinition<User>(
                new BsonDocument(
                        new List<BsonElement> {
                            new BsonElement("Password", password),
                            new BsonElement("Email", email)
                        }
                    )
            )
        );

        return userFilter.FirstOrDefault();
     }

     public async Task<User?> findOne(string email) {
        var userFilter = await UserCollection.FindAsync<User>(
            new BsonDocumentFilterDefinition<User>(
                new BsonDocument(
                        new List<BsonElement> {
                            new BsonElement("Email", email)
                        }
                    )
            )
        );

        return userFilter.FirstOrDefault();
     }

     public async Task<long> createOne(string username, string email, string password) {
         var id = await UserCollection.EstimatedDocumentCountAsync();

         Console.WriteLine(id);

         id += 1;

         var user = new User {
             Email = email,
             UserId = id,
             Username = username,
             Password = password,
         };

         user.ObjectId = ObjectIdGenerator.Instance.GenerateId("users", user).ToString()!;

        await UserCollection.InsertOneAsync(user);
     
        return id;
     } 

}