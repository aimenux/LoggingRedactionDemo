using Example01;
using Example01.Features.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .AddServices()
    .Build();

var userService = host.Services.GetRequiredService<IUserService>();
var logger = host.Services.GetRequiredService<ILogger>();
var user = userService.GetUserById(Random.Shared.Next());
logger.LogUserRetrieved(user.FirstName, user.LastName);
logger.LogUserRetrieved(user.FullName);
logger.LogUserRetrieved(user);

Console.WriteLine("Press any key to exit !");
Console.ReadKey();