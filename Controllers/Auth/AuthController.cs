namespace PizzaAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Services;
using PizzaAPI.Models;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    [HttpPost("register")]
    public async Task<RequestResultBase> register(
        [FromServices] JwtService jwtService,
        [FromServices] AuthService authService,
        [FromServices] OrderService orderService,
        [FromBody] RegisterModel data
    ) {
        var user = await authService.findOne(data.Email);

        if (user != null)
            return new ErrorResult {
                Result = "error",
                Code = 400,
                ErrorMessage = "This email already used."
            }; 


        var userId = await authService.createOne(data.Username, data.Email, data.Password);

        orderService.CreateOrdersCollection(userId);

        var token = jwtService.GenerateToken(data.Email, userId); 

        return new GenerateTokenModel {
            Result = "success",
            Token = token,
            Code = 200
        };
    }

    [HttpPost("signIn")]
    public async Task<RequestResultBase> signIn(
        [FromServices] JwtService jwtService,
        [FromServices] AuthService authService,
        [FromBody] SignInModel data
    ) {
        var user = await authService.findOne(data.Email, data.Password);

        if (user == null)
            return new ErrorResult {
                Result = "error",
                Code = 400,
                ErrorMessage = "Wrong email or password."
            }; 

        var token = jwtService.GenerateToken(data.Email, user.UserId); 

        return new GenerateTokenModel {
            Result = "success",
            Token = token,
            Code = 200
        };
    }
}