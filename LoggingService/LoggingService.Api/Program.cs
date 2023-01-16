using Serilog;
using LoggingService.Api.Dto;
using LoggingService.Api.Services;
using LoggingService.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

LogConfiguration.ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddTransient<ILogService, LogService>();

var app = builder.Build();

app.MapPost("/logInformation", async (LogRequest request, ILogService service) => {
    try
    {
        await service.LogInformation(request);
        return Results.Ok();
    }
    catch { return Results.Problem(); }
});

app.MapPost("/logError", async (LogRequest request, ILogService service) => {
    try
    {
        await service.LogError(request);
        return Results.Ok();
    }
    catch { return Results.Problem(); }
    
});

app.Run();
