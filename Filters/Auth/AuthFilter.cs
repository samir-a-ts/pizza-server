namespace PizzaAPI.Filters;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Services;
using PizzaAPI.Controllers;

public class Authorize : IAuthorizationFilter
{
    private JwtService _service;

    public Authorize(
        JwtService service
    ) {
        _service = service;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
         var authorization = context.HttpContext.Request.Headers.Authorization.ToString();

        var token = authorization.Split(' ')[1];

        if (!_service.CheckToken(token))
            context.Result = new JsonResult(
                new ErrorResult {
                    Code = 401,
                    ErrorMessage = "Not valid token!",
                    Result = "error"
                }
            );
    }
}