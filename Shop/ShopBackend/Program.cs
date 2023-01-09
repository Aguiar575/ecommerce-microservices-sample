using Microsoft.EntityFrameworkCore;
using ShopBackend.Context;
using ShopBackend.Infrastructure;
using ShopBackend.Models;
using ShopBackend.Services;

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
