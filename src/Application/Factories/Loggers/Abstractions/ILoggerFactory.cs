using Application.Loggers.Abstractions;

namespace Application.Factories.Loggers.Abstractions;

public interface ILoggerFactory
{
    enum LoggerType
    {
        Console,
        File
    }
    ILogger CreateLogger();
}
