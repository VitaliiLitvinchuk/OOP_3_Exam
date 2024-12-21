namespace Application.Loggers.Abstractions;

public interface ILogger
{
    enum LogLevel
    {
        Info,
        Error,
        Warning
    }
    Task Log(string message);
    Task LogError(string message);
    Task LogWarning(string message);
}
