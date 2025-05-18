using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace OrderService;

class Program
{
    static ActivitySource activitySource = new("OrderService");
    static HttpClient httpClient = new();

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
            ShouldListenTo = source => true,
            Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStarted = activity => logger.LogInformation("Task init: {name}", activity.DisplayName),
            ActivityStopped = activity => logger.LogInformation("Task completed: {name}", activity.DisplayName)
        };

        ActivitySource.AddActivityListener(listener);
        
        using (var activity = activitySource.StartActivity("ProccessingOrder", ActivityKind.Client))
        {
            if (activity is not null)
            {
                activity.SetTag("order.id", "12345");
                activity.AddBaggage("client.id", "abc-999");

                logger.LogInformation("Init order...");

                var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5001/pagamento");
                request.Headers.Add("traceparent", activity.Id); // W3C trace context
                httpClient.SendAsync(request).GetAwaiter().GetResult();
            }
        }
    }
}
