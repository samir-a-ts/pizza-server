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
        int count = model.MenuItemsId.Count();

        if ((model.ComboId == null && count == 0) || (model.ComboId != null && count > 0)) {
            return new ErrorResult {
                Code = 400,
                Result = "wrong_parameters",
                ErrorMessage = "Cannot order both combo and items from menu or none of them."
            };
        }

        DateTime dateFilter = DateTime.Parse(model.Date, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

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

        
    }
}