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
                Code = 400,
                ErrorMessage = "This email already used."
            }; 


        var userId = await authService.createOne(data.Username, data.Email, data.Password);

        orderService.CreateOrdersCollection(userId);

        var (token, refreshToken) = jwtService.GenerateToken(data.Email!, userId); 

        return new GenerateTokenModel {
            Token = token,
            RefreshToken = refreshToken,
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
                Code = 400,
                ErrorMessage = "Wrong email or password."
            }; 

        var (token, refreshToken) = jwtService.GenerateToken(data.Email!, user.ObjectId!); 

        return new GenerateTokenModel {
            Token = token,
            RefreshToken = refreshToken,
            Code = 200
        };
    }

    [HttpPost("refreshToken")]
    public RequestResultBase refreshToken(
        [FromServices] JwtService jwtService,
        [FromBody] TokenRefreshModel data
    ) {
        if (jwtService.ValidateToken(data.RefreshToken!)) {
            var refreshedToken = jwtService.RefreshToken(data.RefreshToken!);

            return new RefreshedTokenModel {
                Code = 200,
                Token = refreshedToken
            };
        } else {
            return new ErrorResult {
                Code = 400,
                ErrorMessage = "Invalid refresh token."
            }; 
        }     
    }
}