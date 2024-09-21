using Example02.Extensions;
using Example02.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonLogger();
builder.Logging.AddRedaction();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRedactionService, RedactionService>();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseSwaggerDoc();

app.UseHttpsRedirection();

app.MapGet("/users/{userid:int}", (IUserService userService, ILogger logger, int userId) =>
{
    var user = userService.GetUserById(userId);
    logger.LogUserRetrieved(user);
    return Results.Ok(user);
})
.WithName("GetUser")
.WithOpenApi();

app.MapGet("/users/{userid:int}/redaction", (IUserService userService, IRedactionService redactionService, ILogger logger, int userId) =>
{
    var user = userService.GetUserById(userId);
    logger.LogUserRetrieved(user);
    var result = new
    {
        ReadactedFirstName = redactionService.RedactPersonalData(user.FirstName),
        ReadactedLastName = redactionService.RedactPersonalData(user.LastName),
        ReadactedLogin = redactionService.RedactSensitiveData(user.Login),
        ReadactedPassword = redactionService.RedactSensitiveData(user.Password)
    };
    return Results.Ok(result);
})
.WithName("GetUserRedaction")
.WithOpenApi();

await app.RunAsync();
