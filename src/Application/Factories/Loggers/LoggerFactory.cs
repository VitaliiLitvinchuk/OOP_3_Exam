using Application.Factories.Loggers.Abstractions;
using Application.Loggers;
using Application.Loggers.Abstractions;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using static Application.Factories.Loggers.Abstractions.ILoggerFactory;

namespace Application.Factories.Loggers;

public class LoggerFactory : ILoggerFactory
{
    private readonly Dictionary<string, Func<ILogger>> _factoryTemplates = [];
    private readonly IConfiguration _configuration;

    public LoggerFactory(IConfiguration configuration)
    {
        _configuration = configuration;

        _factoryTemplates.Add(Enum.GetName(LoggerType.Console)!, CreateConsoleLogger);
        _factoryTemplates.Add(Enum.GetName(LoggerType.File)!, CreateFileLogger);
    }

    public ILogger CreateLogger()
    {
        var loggerSetting = _configuration.GetSection(nameof(LoggerSetting)).Get<LoggerSetting>()!;

        if (!_factoryTemplates.TryGetValue(loggerSetting.Type!, out var factory))
            throw new ArgumentException($"Logger type {loggerSetting.Type} is not supported", nameof(loggerSetting.Type));

        return factory();
    }

    private static ILogger CreateConsoleLogger()
    {
        return new ConsoleLogger();
    }

    private static ILogger CreateFileLogger()
    {
        return new FileLogger();
    }
}
