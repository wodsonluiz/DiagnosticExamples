namespace SampleApplication.Console;

using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Create and configure an ActivityListener to listen for activities from "MyApp"
        var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "MyApp",
            Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllData,
            ActivityStarted = activity => Console.WriteLine("Activity started: {0}", activity.DisplayName),
            ActivityStopped = activity => Console.WriteLine("Activity stopped: {0}", activity)
        };

        ActivitySource.AddActivityListener(listener);

        using (var activitySource = new ActivitySource("MyApp"))
        {
            using (var activity = activitySource.StartActivity("ProccessOrder", ActivityKind.Internal))
            {
                if (activity is not null)
                {
                    Console.WriteLine("TraceId: {0}", activity.TraceId);
                    Console.WriteLine("SpanId: {0}", activity.SpanId);
                    Console.WriteLine("ParentSpanId: {0}", activity.ParentSpanId);

                    activity.SetTag("user", "joao");
                    activity.SetTag("order", 123);

                    activity.AddBaggage("correlation.client", Guid.NewGuid().ToString());
                    var valor = activity.GetBaggageItem("currelation.client");
                    Console.WriteLine("Baggage: correlation.client: {0}", valor);

                    var evt = new ActivityEvent("OrderValited");
                    activity.AddEvent(evt);

                    Console.WriteLine("Order proccessing...");
                    Thread.Sleep(1000);

                    activity.AddEvent(new ActivityEvent("Order completed"));

                }
            }
        }
    }
}
