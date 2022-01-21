using PizzaAPI.Injection;

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

Injection.Init(builder.Services, builder.Configuration);

var app = builder.Build();

// app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseStaticFiles(
    new StaticFileOptions
    {
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