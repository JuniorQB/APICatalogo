
namespace APICatalogo.Logging;

public class CustomLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerName = loggerName;
        this.loggerConfig = loggerConfig;
    }

    public IDisposable BeginScope<TState>(TState state) 
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

   public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
       Exception exception, Func<TState, Exception, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        WriteTextInFile(message);
    }

    private void WriteTextInFile(string message)
    {
        string filePath = @"C:\temp\ApiLog.txt";

        using (StreamWriter sw = new StreamWriter(filePath, true)) { 
            try
            {
                sw.WriteLine(message);
                sw.Close();
            }catch (Exception e)
            {
                throw;
            }
        }
    }
}
