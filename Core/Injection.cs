namespace PizzaAPI.Injection;


using PizzaAPI.Config;
using PizzaAPI.Models;
using PizzaAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;

public class Injection {

    public static void Init(IServiceCollection Collection, IConfiguration Configuration) {
        var mongoDBOptions = new MongoDBOptions();

        ConfigurationBinder.Bind(Configuration, MongoDBOptions.Position, mongoDBOptions);

        var builder = new MongoDB.Driver.MongoUrlBuilder(mongoDBOptions.Url);

        var client = new MongoDB.Driver.MongoClient(builder.ToMongoUrl());

        var database = client.GetDatabase(mongoDBOptions.DatabaseName);

        var pizzaMenuCollection = database.GetCollection<Pizza>("pizza");
        
        var comboMenuCollection = database.GetCollection<Combo>("combo");

        Collection.AddSingleton<MenuService>(
            new MenuService(pizzaMenuCollection, comboMenuCollection)
        );

        var jwtSecret = Configuration["Jwt:Secret"];

        var jwtOptions = new JwtOptions();

        ConfigurationBinder.Bind(Configuration, JwtOptions.Position, jwtOptions);

        var signingKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(jwtSecret)
        );

        Collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = jwtOptions.Issuer,
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = jwtOptions.Audience,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
                            // установка ключа безопасности
                            IssuerSigningKey = signingKey,
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true
                    };
                }
            );

        Collection.AddSingleton<JwtService>(
            new JwtService(
                new JwtHeader(
                    new SigningCredentials(
                        signingKey,
                        SecurityAlgorithms.HmacSha512Signature
                    )
                )
            )
        );

        Collection.AddControllersWithViews();
    }
}