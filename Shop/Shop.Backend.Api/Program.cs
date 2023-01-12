using Microsoft.EntityFrameworkCore;
using Shop.Backend.Api.Context;
using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;
using Shop.Backend.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IRepository<ProductModel>, Repository<ProductModel>>();
builder.Services.AddTransient<ISnowflakeService, SnowflakeService>();
builder.Services.AddHttpClient<ISnowflakeService, SnowflakeService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddDbContext<ShopBackendContext>(options => 
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ShopBackendContext>();
    context.Database.EnsureCreated();
}

app.MapGet("/products", async (IProductService productService) =>
{
    IEnumerable<ProductModel>? product = await productService.GetProduct();
    return Results.Ok(product);
});

app.MapPost("/product", async (ProductCreate input, IProductService productService) =>
{
    ProductModel? product = await productService.CreateProduct(input);
    return Results.Created($"/product/{product?.Id}", product);
});

app.MapGet("/product/{id}", async (ulong id, IProductService productService) =>
{
    ProductModel? product = await productService.GetProduct(id);
    if(product is null) return Results.NotFound();
    return Results.Ok(product);
});

app.MapPut("/product", async (ProductUpdate productUpdate, IProductService productService) =>
{
    await productService.UpdateProduct(productUpdate);
    return Results.Ok();
});

app.MapDelete("/product/{id}", async (ulong id, IProductService productService) =>
{
    await productService.DeleteProduct(id);
    return Results.Ok();
});
app.Run();
