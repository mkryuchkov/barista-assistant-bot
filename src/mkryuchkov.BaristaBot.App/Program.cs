using mkryuchkov.BaristaBot.TgBot;
using mkryuchkov.BaristaBot.TgBot.Extensions;
using mkryuchkov.Logging;
using mkryuchkov.TgBot;
using mkryuchkov.TgBot.Configuration;
using mkryuchkov.TgBot.Controllers;
using Serilog;

Log.Logger = LoggerCreator.CreateDefaultLogger();
Log.Information("Starting");

try
{
    Log.Information("Building");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddTgBot(builder.Configuration);
    builder.Services.AddScoped<ITgUpdateHandler, TgUpdateHandler>();
    builder.Services.AddBot();

    var app = builder.Build();

    app.MapWebhookRoute(app.Configuration);

    Log.Information("Running");
    app.Run();
    Log.Information("Finished");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}