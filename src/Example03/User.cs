using Example03.Extensions;

namespace Example03;

public sealed record User
{
    [SensitiveData]
    public required string Login { get; init; }
    
    [SensitiveData]
    public required string Password { get; init; }
    
    [PersonalData]
    public required string FirstName { get; init; }
    
    [PersonalData]
    public required string LastName { get; init; }
}