namespace Example02.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseSwaggerDoc(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}