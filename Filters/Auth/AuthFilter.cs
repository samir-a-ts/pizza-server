namespace PizzaAPI.Filters;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
    }

}