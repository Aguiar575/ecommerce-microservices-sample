using LoggingService.Dto;

namespace LoggingService.Services;

class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;

    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }

    public async Task LogInformation(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogInformation($"Log Level: Information ApplicationName: {request.ApplicationName} " +
                                   $"Message: {request.Message} " +
                                   $"Date: {request.Date}"));
    }

    public async Task LogError(LogRequest request)
    {
        await Task.Run(() => 
            _logger.LogInformation($"Log Level: Information ApplicationName: {request.ApplicationName} " +
                                   $"Message: {request.Message} " +
                                   $"InnerMessage: {request.InnerMessage} " +
                                   $"Stacktrace: {request.StackTrace} " +
                                   $"Date: {request.Date}"));
    }
}