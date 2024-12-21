using Application.Loggers.Abstractions;

namespace Application.Loggers;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger()
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(root, "log.txt");
    }

    public async Task Log(string message)
    {
        try
        {
            string logEntry = $"{DateTime.Now}: {message}";

            await File.AppendAllTextAsync(_filePath, $"{logEntry}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while writing to the log file: {ex.Message}");
        }
    }

    public async Task LogError(string message)
    {
        await Log($"[ERROR] {message}");
    }

    public async Task LogWarning(string message)
    {
        await Log($"[WARNING] {message}");
    }
}

