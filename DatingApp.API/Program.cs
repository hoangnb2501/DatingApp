using System.Text.Json.Serialization;
using DatingApp.API.Data;
using DatingApp.API.Extensions;
using DatingApp.API.Helpers;
using DatingApp.API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    });
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);
}

var app = builder.Build();

{
    // Configure the HTTP request pipeline.
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseCors(builder =>
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("https://localhost:4200"));

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatingAppDbContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedUser(context);
    }
    catch (Exception exception)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred during migration");
    }

    app.Run();
}
