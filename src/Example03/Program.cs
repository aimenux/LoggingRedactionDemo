using System.Text.Json;
using Example03;
using Example03.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((_, loggingBuilder) =>
    {
        loggingBuilder.AddLogging();
        loggingBuilder.EnableRedaction();
    })
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddJsonConsole(options =>
            {
                options.JsonWriterOptions = new JsonWriterOptions
                {
                    Indented = true
                };
            });
            builder.EnableRedaction();
            builder.Services.AddRedaction(new RedactionSettings
            {
                KeyId = 1,
                Key = "XYA94357E89745E698D66FDD1BCBBE3CDA4E4E8D65XY"
            });
            builder.Services.AddSingleton(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger("Example03");
            });
        });
        services.AddSingleton<IService, Service>();
    })
    .Build();

var service = host.Services.GetRequiredService<IService>();
var user = service.CreateUser();

var logger = host.Services.GetRequiredService<ILogger>();
logger.LogUserCreated(user);

