using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Example03.Extensions;

public static partial class LoggingExtensions
{
    public static void AddLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        
        loggingBuilder.AddJsonConsole(options =>
        {
            options.JsonWriterOptions = new JsonWriterOptions
            {
                Indented = true
            };
        });
        
        loggingBuilder.Services.AddSingleton(serviceProvider =>
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger("Example03");
        });
    }
    
    [LoggerMessage(LogLevel.Information, "User created")]
    public static partial void LogUserCreated(this ILogger logger, [LogProperties] User user);
}