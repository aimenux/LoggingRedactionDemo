using Example01.Features.Redaction;
using Microsoft.Extensions.Logging;

namespace Example01.Features.Users;

public sealed record User
{
    public int Id { get; init; }
    
    [Sensitive]
    public required string Login { get; init; }
    
    [Sensitive]
    public required string Password { get; init; }
    
    [Personal]
    public required string FirstName { get; init; }
    
    [Personal]
    public required string LastName { get; init; }
    
    [LogPropertyIgnore]
    public string FullName => $"{FirstName} {LastName}";
    
    [Default]
    public required string UserAgent { get; init; }

    [Restricted] 
    public string[] Roles { get; init; } = [];
    
    [Restricted]
    public DateTime? LastActivityDate { get; init; }
}