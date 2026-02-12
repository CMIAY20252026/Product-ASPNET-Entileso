using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Product API", Version = "v1" });
});

var app = builder.Build();

// Always enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
    c.RoutePrefix = string.Empty; // Swagger at root
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Use dynamic ports from environment variables if available
var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ??
           "http://0.0.0.0:0;https://0.0.0.0:0"; // 0 = OS chooses free port
foreach (var url in urls.Split(';', StringSplitOptions.RemoveEmptyEntries))
{
    app.Urls.Add(url);
}

app.Run();
