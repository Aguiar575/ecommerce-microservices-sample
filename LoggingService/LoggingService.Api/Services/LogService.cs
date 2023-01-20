using LoggingService.Api.Dto;

namespace LoggingService.Api.Services;

public class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;

    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }

    public async Task LogInformation(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogInformation(CreateLogMessageWithoutException(request, "Information")));
    }

    public async Task LogTracing(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogTrace(CreateLogMessageWithoutException(request, "Tracing")));
    }

    public async Task LogWarning(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogWarning(CreateLogMessageWithoutException(request, "Warning")));
    }

    public async Task LogError(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogError($"Log Level: Error ApplicationName: {request.ApplicationName} " +
                                   $"Message: {request.Message} " +
                                   $"InnerMessage: {request.InnerMessage} " +
                                   $"Stacktrace: {request.StackTrace} " +
                                   $"Date: {request.Date}"));
    }

    private string CreateLogMessageWithoutException(LogRequest request, string level) => 
        $"Log Level: {level} ApplicationName: {request.ApplicationName} " +
        $"Message: {request.Message} " +
        $"Date: {request.Date}";
}