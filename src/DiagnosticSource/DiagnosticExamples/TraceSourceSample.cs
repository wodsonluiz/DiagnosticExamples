using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace DiagnosticExamples
{
    public class TraceSourceSample
    {
        static TraceSource _ts = new TraceSource("TraceSourceSamples");

        public TraceSourceSample()
        {
            SourceSwitch sourceSwitch = new SourceSwitch("mySwitch");
            sourceSwitch.Level = SourceLevels.Information;
            _ts.Switch = sourceSwitch;

            //Create a consoletracelistener and add it to the trace listerns collection
            var listener = new ConsoleTraceListener();
            int tsConsole = _ts.Listeners.Add(listener);
            _ts.Listeners[tsConsole].Name = "Console";

            //Create a XmlWriterTraceListener and add it to the TraceSource listeners collection
            XmlWriterTraceListener xmlListener = new XmlWriterTraceListener(
                Path.Combine(Environment.CurrentDirectory, "XmlWriterTraceListener_output.xml")
            );

            int tsXml = _ts.Listeners.Add(xmlListener);
            _ts.Listeners[tsXml].Name = "Xml";

            //Configure Options and Filters on TraceListeners
            _ts.Listeners["Console"].TraceOutputOptions = TraceOptions.DateTime;
            _ts.Listeners["Console"].Filter = new EventTypeFilter(SourceLevels.Information);
            _ts.Listeners["Xml"].Filter = new EventTypeFilter(SourceLevels.Warning);

            //Autoflush required for writing to xml file
            Trace.AutoFlush = true;
        }

        public void RunSample()
        {
            _ts.TraceEvent(TraceEventType.Verbose, 0, "In RunSample, about to call PerformCalculation");
            string result = PerformCalculation("5", "w");

            Console.WriteLine("The result is " + result); 
        }

        private string PerformCalculation(string firstNumber, string secondNumer)
        {
            _ts.TraceEvent(TraceEventType.Information, 1, "Entering {0} method. ", "PerfomCalculation");

            try
            {
                _ts.TraceInformation("About to perfom calculation...");

                int value = int.Parse(firstNumber) + int.Parse(secondNumer);
                
                return value.ToString();
            }
            catch (Exception ex)
            {
                string errorContext = "firstNumber:" + firstNumber + " - secondNumber: " + secondNumer;
                _ts.TraceData(TraceEventType.Error, 3, new object[] { errorContext, ex});

                return null;
            }
        }
    }    

}