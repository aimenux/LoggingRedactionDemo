namespace Example02.Features.Users;

public static class UserEndpoints
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/users", GetUsers)
            .WithName("GetUsers")
            .WithSummary("Retrieves a list of users")
            .WithDescription("Gets all users")
            .Produces<IEnumerable<User>>(contentType: "application/json");
    }

    private static IResult GetUsers(IUserService userService, ILogger<Program> logger)
    {
        var user = userService.GetUserById(Random.Shared.Next());
        logger.LogWaysForUserRetrieved(user);
        return Results.Ok(new[] { user });
    }
}