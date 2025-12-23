namespace Example02.Features.Users;

public static partial class UserExtensions
{
    [LoggerMessage(LogLevel.Information, "User retrieved")]
    public static partial void LogUserRetrieved(this ILogger logger, [LogProperties] User user);
}