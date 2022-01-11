using PizzaAPI.Injection;

using Microsoft.Extensions.FileProviders;

using Microsoft.Extensions.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);

Injection.Init(builder.Services, builder.Configuration);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles(
    new StaticFileOptions {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(
                builder.Environment.ContentRootPath, "Public"
            )
        ),
        RequestPath = "/static"
    }
);

app.MapControllers();

app.Run();