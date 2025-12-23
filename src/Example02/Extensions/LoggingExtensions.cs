using System.Text.Json;
using Example02.Redaction;

namespace Example02.Extensions;

public static class LoggingExtensions
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
            var categoryName = typeof(Program).Assembly.GetName().Name!;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(categoryName);
        });
        
        loggingBuilder.EnableRedaction();
        
        loggingBuilder.Services.AddRedaction(RedactionConfiguration.Build());
    }
}