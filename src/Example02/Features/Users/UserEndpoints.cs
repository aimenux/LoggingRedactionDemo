namespace Example02.Features.Users;

public static class UserEndpoints
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/users", GetUsers)
            .WithName("GetUsers")
            .WithOpenApi();
    }

    private static IResult GetUsers(IUserService userService, ILogger<Program> logger)
    {
        var user = userService.GetUserById(Random.Shared.Next());
        logger.LogWaysForUserRetrieved(user);
        return Results.Ok(new[] { user });
    }
}