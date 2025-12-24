using Example02;
using Example02.Extensions;
using Example02.Features.Users;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseSwaggerDoc();

app.UseHttpsRedirection();

app.MapUsersEndpoints();

await app.RunAsync();
