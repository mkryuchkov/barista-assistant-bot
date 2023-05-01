using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace mkryuchkov.Logging;

public static class LoggerCreator
{
    public static ILogger CreateDefaultLogger()
    {
        var config = new LoggerConfiguration()
            .Enrich.FromLogContext();


        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            config.MinimumLevel.Debug();
        }
        else
        {
            config.MinimumLevel.Information();
        }

        config
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .MinimumLevel.Override("System", LogEventLevel.Warning);

        config.WriteTo.Console(new CompactJsonFormatter());

        return config.CreateLogger();
    }
}