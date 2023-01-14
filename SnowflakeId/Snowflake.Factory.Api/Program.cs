using Microsoft.Extensions.Options;
using Snowflake.Factory.Dto;
using Snowflake.Factory.Model;
using Snowflake.Factory.Provider;
using Snowflake.Factory.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SnowflakeModel>(builder.Configuration.GetRequiredSection("SnowflakeId"));
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<SnowflakeModel>(x => x.GetRequiredService<IOptions<SnowflakeModel>>().Value);
builder.Services.AddSingleton<SnowflakeIdService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/snowflake-id", (SnowflakeIdService snowflakeIdService) => 
    Results.Ok(new SnowflakeId(snowflakeIdService.CreateSnowflakeId())))
    .WithName("SnowflakeId")
    .Produces(StatusCodes.Status200OK, typeof(SnowflakeId));

app.Run();
