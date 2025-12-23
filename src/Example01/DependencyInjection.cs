using Example01.Extensions;
using Example01.Features.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example01;

public static class DependencyInjection
{
    public static IHostBuilder AddServices(this IHostBuilder builder)
    {
        return builder
            .ConfigureLogging((_, loggingBuilder) =>
            {
                loggingBuilder.AddLogging();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IUserService, UserService>();
            });
    }
}