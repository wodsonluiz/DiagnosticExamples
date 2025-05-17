using System.Diagnostics;

namespace DiagnosticExamples.Library
{
    public class SampleEvents
    {
        private static DiagnosticSource sampleLogger = new DiagnosticListener("DiagnosticExamples.Library");
    
        public void ReiseEvent()
        {
            if(sampleLogger.IsEnabled("SampleEvent"))
            {
                sampleLogger.Write("SampleEvent", new { name = "Initiated", info = "Some info about the event"});
            }
        }
    }
}