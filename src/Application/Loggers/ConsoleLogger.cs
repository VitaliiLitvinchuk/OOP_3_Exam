using Application.Loggers.Abstractions;

namespace Application.Loggers;

public class ConsoleLogger : ILogger
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task Log(string message)
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task LogError(string message)
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task LogWarning(string message)
    {
        await _semaphore.WaitAsync();
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
