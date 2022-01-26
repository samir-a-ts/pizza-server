namespace PizzaAPI.Controllers;

using PizzaAPI.Services;
using PizzaAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Globalization;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public RequestResultBase order(
        [FromServices] JwtService jwtService,
        [FromServices] MenuService menuService,
        [FromBody] OrderModel model
    ) {


        if (model.Address!.Length == 0) {
            return new ErrorResult {
                Code = 400,
                Result = "wrong_parameters",
                ErrorMessage = "Client address not provided."
            };
        }

        if (model.PhoneNumber!.Length == 0) {
            return new ErrorResult {
                Code = 400,
                Result = "wrong_parameters",
                ErrorMessage = "Client phone number not provided."
            };
        }

        if (model.MenuItemsId!.Count() == 0) {
            return new ErrorResult {
                Code = 400,
                Result = "wrong_parameters",
                ErrorMessage = "Cannot create empty order."
            };
        }

        var comboCount = 0;
        var pizzaCount = 0;

        foreach (var item in model.MenuItemsId!)
        {
            switch (item.ItemName)
            {
                case "pizza":
                    pizzaCount++;
                    break;
                case "combo":
                    comboCount++;
                    break;
                default:
                    return new ErrorResult {
                        Code = 400,
                        Result = "wrong_parameters",
                        ErrorMessage = "Unknown item type."
                    };
            }

            if (comboCount > 0 && pizzaCount > 0) {
                return new ErrorResult {
                        Code = 400,
                        Result = "wrong_parameters",
                        ErrorMessage = "Cannot order both combos and pizzas."
                    };
            }
        }

        DateTime dateFilter = DateTime.Parse(
            model.Date!,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal
        );

        if (dateFilter <  DateTime.Now) {
            return new ErrorResult {
                Code = 400,
                Result = "wrong_parameters",
                ErrorMessage = "Cannot create order in the past."
            };
        }

        var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

        var parsed = jwtService.ParseToken(token);

        var id = parsed.Claims.ElementAt(1).Value;

        return default;
    }
}