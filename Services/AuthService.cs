namespace PizzaAPI.Services;

using MongoDB.Driver;
using MongoDB.Bson;

using PizzaAPI.Models;

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

     public async Task createOne(string username, string email, string password) {
         var id = await UserCollection.EstimatedDocumentCountAsync();

         id += 1;

         var user = new User {
             Email = email,
             Id = id,
             Username = username,
             Password = password,
         };

        # return UserCollection.InsertOneAsync(user);
     } 

}