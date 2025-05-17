using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading;

namespace DiagnosticExamples
{
    public sealed class DiagnosticExampleObserver : IObserver<DiagnosticListener>, IObserver<KeyValuePair<string, object>>
    {
        private readonly AsyncLocal<Stopwatch> _stopwatch = new AsyncLocal<Stopwatch>();

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(DiagnosticListener value)
        {
            Console.WriteLine("DiagnosticSource Found. Name: {0}", value.Name);
        
            if(value.Name == "DiagnosticExamples.Library")
            {
                var subscription = value.Subscribe(this);
            }
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            Console.WriteLine("Event Name: " + value.Key);
            Console.WriteLine("Event Value: " + value.Value);
            Console.WriteLine();
            Console.WriteLine("---------------------------------");
        
            switch (value.Key)
            {
                case "System.Net.Http.Request":
                    {
                        Type t = value.Value.GetType();
                        PropertyInfo property = t.GetProperty("Request");
                        var req = (HttpRequestMessage)property.GetValue(value.Value);
                        req.Headers.Add("CustomHeader", "Value Test"); //Can change modify object. !!! This not recomendation

                        Console.WriteLine("Request URI is {0}", req.RequestUri.ToString());
                        Console.WriteLine("Request Method is {0}", req.Method.ToString());

                        _stopwatch.Value = Stopwatch.StartNew();
                        break;
                    }
                case "System.Net.Http.Response": 
                    {
                        var stopwatch = _stopwatch.Value;
                        stopwatch.Stop();

                        Console.WriteLine("The HttpClient call took in {0} in seconds", stopwatch.Elapsed.TotalSeconds);
                        break;
                    }
            }
        }
    }
}