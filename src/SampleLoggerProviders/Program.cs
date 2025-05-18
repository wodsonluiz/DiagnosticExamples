using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleLoggerProviders;

class Program
{
    static void Main(string[] args)
    {
        var configBulder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", true, true);

        var config = configBulder.Build();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                //.AddFilter("ConsoleLoggerProviderSamples", LogLevel.Information)
                .AddConfiguration(config.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                .AddEventSourceLogger();
        });

        ILogger logger = loggerFactory.CreateLogger<Program>();

        logger.Log(LogLevel.Debug, "This is a debug message from {0}", "Program.Main");
        logger.Log(LogLevel.Trace, "This is a trace message from {0}", "Program.Main");

        logger.LogInformation("Information message from Program.Main");
        logger.LogWarning("Warning Message from Program.Main");
        logger.LogError(12, "Error Message from Program.Main");
    }
}
