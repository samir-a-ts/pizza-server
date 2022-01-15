namespace PizzaAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Services;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    [HttpGet("test")]
    public async Task<GenerateTokenModel> test([FromServices] JwtService service) {
        var token = await service.GenerateToken("test", 2739243);

        return new GenerateTokenModel {
            Result = "success",
            Token = token
        };
    }
}