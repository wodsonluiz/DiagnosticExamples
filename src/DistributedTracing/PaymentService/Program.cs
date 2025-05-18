using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace PaymentService;

class Program
{
    static ActivitySource activitySource = new("PaymentService");
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
            ShouldListenTo = _ => true,
            Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStarted = a => logger.LogInformation("Payment init: {name}", a.DisplayName),
            ActivityStopped = a => logger.LogInformation("Payment finish: {name}", a.DisplayName)
        };
        ActivitySource.AddActivityListener(listener);

        // Simulando um servidor
        var server = new HttpListener();
        server.Prefixes.Add("http://localhost:5001/");
        server.Start();
        logger.LogInformation("PaymentService pronto em http://localhost:5001");
        
        while (true)
        {
            var context =  server.GetContextAsync().GetAwaiter().GetResult();
            var traceparent = context.Request.Headers["traceparent"];
            var ctx = ActivityContext.Parse(traceparent, null);

            using var activity = activitySource.StartActivity("ValidationPayment", ActivityKind.Server, ctx);
            activity?.SetTag("order.status", "approved");

            logger.LogInformation("Payment authorized for order: {pedidoId}", activity?.GetTagItem("order.id"));

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5002/notificar");
            request.Headers.Add("traceparent", activity?.Id);
            httpClient.SendAsync(request).GetAwaiter().GetResult();

            context.Response.StatusCode = 200;
            context.Response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Payment OK"))
                .GetAwaiter()
                .GetResult();

            context.Response.Close();
        }

    }
}
