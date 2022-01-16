namespace PizzaAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Services;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    [HttpPost("register")]
    public async Task<GenerateTokenModel> register([FromServices] JwtService service, string username, string password, string email) {
        var token =  service.GenerateToken("test", 2739243);

        return new GenerateTokenModel {
            Result = "success",
            Token = token
        };
    }
}