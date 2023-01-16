using LoggingService.Api.Dto;

namespace LoggingService.Api.Services;

public interface ILogService
{
    Task LogInformation(LogRequest request);
    Task LogError(LogRequest request);
}