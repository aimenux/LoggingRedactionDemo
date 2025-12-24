using Example01.Features.Redaction;
using Microsoft.Extensions.Logging;

namespace Example01.Features.Users;

public static partial class UserExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "User retrieved - FirstName: {FirstName}, LastName: {LastName}")]
    public static partial void LogUserRetrieved(this ILogger logger, [Personal] string firstName, [Personal] string lastName);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "User retrieved - FullName: {FullName}")]
    public static partial void LogUserRetrieved(this ILogger logger, [Personal] string fullName);
    
    [LoggerMessage(LogLevel.Information, "User retrieved")]
    public static partial void LogUserRetrieved(this ILogger logger, [LogProperties] User user);
}