using Example01.Features.Redaction;
using Microsoft.Extensions.Logging;

namespace Example01.Features.Users;

public static partial class UserExtensions
{
    public static void LogWaysForUserRetrieved(this ILogger logger, User user)
    {
        // Redaction is not applied through logging scopes ❌
        logger.LogUserRetrievedWithScope(user);
        
        // Redaction is not applied through standard logging methods ❌
        logger.LogInformation("❌ User retrieved - {FirstName} {LastName}", user.FirstName, user.LastName);
        logger.LogInformation("❌ User retrieved - {FullName}", user.FullName);
        logger.LogInformation("❌ User retrieved - {@User}", user);
        logger.LogInformation("❌ User retrieved - {User}", user);

        // Redaction is applied through the source-generated logging methods ✅
        logger.LogUserRetrieved(user.FirstName, user.LastName);
        logger.LogUserRetrieved(user.FullName);
        logger.LogUserRetrieved(user);
    }

    private static void LogUserRetrievedWithScope(this ILogger logger, User user)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["FirstName"] = user.FirstName,
            ["LastName"] = user.LastName,
            ["Login"] = user.Login,
            ["Password"] = user.Password
        });
        
        logger.LogInformation("❌ User retrieved");
    }
    
    [LoggerMessage(Level = LogLevel.Information, Message = "✅ User retrieved - FirstName: {FirstName}, LastName: {LastName}")]
    private static partial void LogUserRetrieved(this ILogger logger, [Personal] string firstName, [Personal] string lastName);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "✅ User retrieved - FullName: {FullName}")]
    private static partial void LogUserRetrieved(this ILogger logger, [Personal] string fullName);
    
    [LoggerMessage(LogLevel.Information, "✅ User retrieved")]
    private static partial void LogUserRetrieved(this ILogger logger, [LogProperties] User user);
}