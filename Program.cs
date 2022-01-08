using PizzaAPI.Injection;

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

Injection.Init(builder.Services, builder.Configuration);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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