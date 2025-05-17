using System;
using System.Diagnostics;

namespace DiagnosticExamples
{
    public class DebugSample
    {
        public DebugSample()
        {
            var listener = new ConsoleTraceListener();
            Trace.Listeners.Add(listener);
        }

        public void RunSample()
        {
            string result = PerformCalculation("5", "20");

            Console.WriteLine("The result is " + result);
        }

        public string PerformCalculation(string firstNumber, string secondNumber)
        {
            Trace.WriteLine("Entering private method to add strings.", "Tracing");
            Trace.Indent();

            Trace.WriteLineIf(int.Parse(secondNumber) > 10, "second number is greater than 10", "Logic log");

            return (int.Parse(firstNumber) + int.Parse(secondNumber)).ToString();
        }
    }
}