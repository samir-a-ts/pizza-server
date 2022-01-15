using PizzaAPI.Injection;

using Microsoft.Extensions.FileProviders;

using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

Injection.Init(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

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