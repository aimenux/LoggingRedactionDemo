using Example02.Features.Redaction;

namespace Example02.Features.Users;

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
}