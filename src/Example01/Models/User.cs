using Example01.Extensions;

namespace Example01.Models;

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
}