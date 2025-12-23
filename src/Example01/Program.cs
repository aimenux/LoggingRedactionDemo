using Example01.Extensions;
using Example01.Features.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((_, loggingBuilder) =>
    {
        loggingBuilder.AddLogging();
    })
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IUserService, UserService>();
    })
    .Build();

var userService = host.Services.GetRequiredService<IUserService>();
var logger = host.Services.GetRequiredService<ILogger>();
var user = userService.GetUserById(Random.Shared.Next());
logger.LogUserRetrieved(user);

Console.WriteLine("Press any key to exit !");
Console.ReadKey();