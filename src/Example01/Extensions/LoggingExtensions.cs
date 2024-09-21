using System.Text.Json;
using Example01.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Example01.Extensions;

public static partial class LoggingExtensions
{
    public static void AddJsonLogger(this ILoggingBuilder loggingBuilder)
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
            var categoryName = typeof(Program).Assembly.GetName().Name!;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(categoryName);
        });
    }
    
    [LoggerMessage(LogLevel.Information, "User retrieved")]
    public static partial void LogUserRetrieved(this ILogger logger, [LogProperties] User user);
}