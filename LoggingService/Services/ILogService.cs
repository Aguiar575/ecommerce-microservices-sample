using LoggingService.Dto;

namespace LoggingService.Services;

public interface ILogService
{
    Task LogInformation(LogRequest request);
    Task LogError(LogRequest request);
}