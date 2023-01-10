using Microsoft.EntityFrameworkCore;
using Shop.Backend.Api.Context;
using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;
using Shop.Backend.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IRepository<ProductModel>, Repository<ProductModel>>();
builder.Services.AddTransient<ISnowflakeService, SnowflakeService>();

builder.Services.AddDbContext<ShopBackendContext>(options => 
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ShopBackendContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.MapGet("/", () => "Hello World!");

app.Run();
