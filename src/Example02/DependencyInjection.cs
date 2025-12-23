using Example02.Extensions;
using Example02.Users;

namespace Example02;

public static class DependencyInjection
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Logging.AddLogging();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSwaggerDoc();
    }
}