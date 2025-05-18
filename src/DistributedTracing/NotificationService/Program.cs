using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Logging;

namespace NotificationService;

class Program
{
    static ActivitySource activitySource = new("NotificationService");

    static void Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
            });
        });

        var logger = loggerFactory.CreateLogger<Program>();

        using var listener = new ActivityListener
        {
            ShouldListenTo = _ => true,
            Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStarted = a => logger.LogInformation("Notification init: {name}", a.DisplayName),
            ActivityStopped = a => logger.LogInformation("Notification finish: {name}", a.DisplayName)
        };

        ActivitySource.AddActivityListener(listener);

        // Simulando um servidor
        var server = new HttpListener();
        server.Prefixes.Add("http://localhost:5002/");
        server.Start();
        logger.LogInformation("NotificationService completed http://localhost:5002");

        while (true)
        {
            var context = server.GetContextAsync()
                .GetAwaiter()
                .GetResult();

            var traceparent = context.Request.Headers["traceparent"];
            var ctx = ActivityContext.Parse(traceparent, null);

            using var activity = activitySource.StartActivity("NotifingOrder", ActivityKind.Server, ctx);
            activity?.SetTag("channel", "email");

            logger.LogInformation("Notifying client by {channel}", activity?.GetTagItem("channel"));

            context.Response.StatusCode = 200;
            context.Response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Notifield"))
                .GetAwaiter()
                .GetResult();

            context.Response.Close();
        }

    }
}
