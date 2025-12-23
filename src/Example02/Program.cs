using Example02;
using Example02.Extensions;
using Example02.Users;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseSwaggerDoc();

app.UseHttpsRedirection();

app.MapGet("/users", (IUserService userService, ILogger logger) =>
{
    var user = userService.GetUserById(Random.Shared.Next());
    logger.LogUserRetrieved(user);
    return Results.Ok(new[]
    {
        user
    });
})
.WithName("GetUsers")
.WithOpenApi();

await app.RunAsync();
