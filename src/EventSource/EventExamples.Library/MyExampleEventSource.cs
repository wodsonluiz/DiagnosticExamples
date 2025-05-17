using System.Diagnostics.Tracing;

namespace EventExamples.Library
{
    [EventSource(Name = "Wod.MyExampleEventSource")]
    public class MyExampleEventSource : EventSource
    {
        public static MyExampleEventSource Log = new MyExampleEventSource();

        [Event(1, Message = "Starting Up.", Level = EventLevel.Informational)]
        public void Startup() => WriteEvent(1);

        [Event(2, Message = "The Calculation will be performed using the values provided.", Level = EventLevel.Informational)]
        public void PerformCalculation(int param1, int param2) =>
            WriteEvent(2, param1, param2);

        [Event(3, Message = "Calculation has completed.", Level = EventLevel.Informational)]
        public void CalculationComplete() => WriteEvent(3);

        [Event(4, Message = "Warning message logged.", Level = EventLevel.Warning)]
        public void WarningMessage(string message) =>
            WriteEvent(4, message);
    }
}