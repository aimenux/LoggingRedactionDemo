using Example01.Extensions;
using Example01.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((_, loggingBuilder) =>
    {
        loggingBuilder.AddJsonLogger();
        loggingBuilder.AddRedaction();
    })
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRedactionService, RedactionService>();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger>();

var userService = host.Services.GetRequiredService<IUserService>();
var user = userService.GetUserById(Random.Shared.Next());
logger.LogUserRetrieved(user);

var redactionService = host.Services.GetRequiredService<IRedactionService>();
logger.LogInformation("Redacted FirstName: {FirstName}", redactionService.RedactPersonalData(user.FirstName));
logger.LogInformation("Redacted LastName: {LastName}", redactionService.RedactPersonalData(user.LastName));
logger.LogInformation("Redacted Login: {Login}", redactionService.RedactSensitiveData(user.Login));
logger.LogInformation("Redacted Password: {Password}", redactionService.RedactSensitiveData(user.Password));

Console.WriteLine("Press any key to exit !");
Console.ReadKey();