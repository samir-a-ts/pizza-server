namespace PizzaAPI.Controllers;

using PizzaAPI.Services;
using PizzaAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("history")]
    public async Task<RequestResultBase> getOrderHistory(
        [FromServices] JwtService jwtService,
        [FromServices] OrderService orderService
    ) {
        var id = _getUserId(jwtService);

        var ordersDocuments = await orderService.GetOrders(id);

        var ordersList = new List<Order>();

        foreach (var item in ordersDocuments)
        {
            var elements = new List<Object>();

            foreach (var document in item.MenuItemsId!)
            {
                var dotNetObj = BsonTypeMapper.MapToDotNetValue(document);

                elements.Add(dotNetObj);
            }

            ordersList.Add(
                new Order {
                    Address = item.Address,
                    Date = item.Date,
                    Description = item.Description,
                    ObjectId = item.ObjectId,
                    PhoneNumber = item.PhoneNumber,
                    MenuItemsId = elements,
                }
            );
        }

        return new OrderHistoryResult {
            Code = 200,
            orders = ordersList, 
        };
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<RequestResultBase> order(
        [FromServices] JwtService jwtService,
        [FromServices] OrderService orderService,
        [FromServices] MenuService menuService,
        [FromBody] OrderModel model
    ) {
        if (model.Address!.Length == 0) {
            return new FieldValidationErrorResult {
                Code = 400,
                FieldName = "address",
                ErrorMessage = "Client address not provided."
            };
        }

        if (model.PhoneNumber!.Length == 0) {
            return new FieldValidationErrorResult {
                Code = 400,
                FieldName = "phoneNumber",
                ErrorMessage = "Client phone number not provided."
            };
        }

        var items = model.MenuItemsId!.Value.EnumerateArray();

        if (items.Count() == 0) {
            return new FieldValidationErrorResult {
                Code = 400,
                FieldName = "menuItemsId",
                ErrorMessage = "Cannot create empty order."
            };
        }

        var combosRequest = menuService.GetCombosList();

        IEnumerable<Pizza>? pizzas = null;
        IEnumerable<Combo>? combos = null;

        var comboCount = 0;
        var pizzaCount = 0;

        foreach (var item in items)
        {
            switch (item.GetProperty("itemName").ToString())
            {
                case "pizza":
                    if (pizzas == null) {
                        pizzas = await menuService.GetPizzaList();
                    }

                    if (pizzas!.Any(pizza => 
                            pizza.ObjectId == item.GetProperty("itemId").ToString()
                            &&  pizza.PriceDictionary!.ContainsKey(
                                    item.GetProperty("size").ToString()
                                )
                            )
                        ) {
                        pizzaCount++;
                    } else {
                        return new FieldValidationErrorResult {
                            Code = 400,
                            FieldName = "menuItemsId",
                            ErrorMessage = "Unknown pizza ID."
                        };
                    }

                    break;
                case "combo":
                    if (combos == null) {
                        combos = await menuService.GetCombosList();
                    }

                    if (combos!.Any(combo => 
                            combo.ObjectId == item.GetProperty("itemId").ToString()
                            )
                        ) {
                        pizzaCount++;
                    } else {
                        return new FieldValidationErrorResult {
                            Code = 400,
                            FieldName = "menuItemsId",
                            ErrorMessage = "Unknown combo."
                        };
                    }

                    comboCount++;
                    break;
                default:
                    return new FieldValidationErrorResult {
                        Code = 400,
                        FieldName = "menuItemsId",
                        ErrorMessage = "Unknown item type."
                    };
            }

            if (comboCount > 0 && pizzaCount > 0) {
                return new FieldValidationErrorResult {
                        Code = 400,
                        FieldName = "menuItemsId",
                        ErrorMessage = "Cannot order both combos and pizzas."
                    };
            }

            if (comboCount > 1) {
                return new FieldValidationErrorResult {
                        Code = 400,
                        FieldName = "menuItemsId",
                        ErrorMessage = "Cannot order a bunch of combos."
                    };
            }
        }

        var dateFilter = DateTime.ParseExact(
            model.Date!,
            "dd/MM/yyyy HH:mm",
            null
        );

        if (dateFilter.CompareTo(DateTime.UtcNow) > 0) {
            return new FieldValidationErrorResult {
                Code = 400,
                FieldName = "date",
                ErrorMessage = "Cannot create order on yesterday."
            };
        }

        var id = _getUserId(jwtService);

        var result = await orderService.CreateOrder(id, model);

        return new OrderSuccessModel {
            Code = 200,
            Order = result,
        };
    }

    private string _getUserId(JwtService jwtService) {
        var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

        var parsed = jwtService.ParseToken(token);

        return parsed.Claims.ElementAt(1).Value;
    }
}