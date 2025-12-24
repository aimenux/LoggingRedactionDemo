using System.Text.Encodings.Web;
using System.Text.Json;
using Example01.Features.Redaction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Example01.Extensions;

public static class LoggingExtensions
{
    public static void AddLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        
        loggingBuilder.AddJsonConsole(options =>
        {
            options.IncludeScopes = true;
            
            options.JsonWriterOptions = new JsonWriterOptions
            {
                Indented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
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