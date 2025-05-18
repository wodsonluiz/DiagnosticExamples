using System;
using System.Diagnostics;
using System.Net.Http;
using DiagnosticExamples;
using DiagnosticExamples.Library;

// var debugSamples = new DebugSample();
// debugSamples.RunSample();

//var traceSourceSample = new TraceSourceSample(); 
//traceSourceSample.RunSample();

var observer = new DiagnosticExampleObserver();
IDisposable subscription = DiagnosticListener.AllListeners.Subscribe(observer);

var lib = new SampleEvents();
lib.ReiseEvent();

//var url = "http://localhost:5052/WeatherForecast";
//var client = new HttpClient();

//var response = await client.GetAsync(url);
//var success = response.Content.ReadAsStringAsync()
//    .GetAwaiter()
//    .GetResult();

//Console.WriteLine("Web Request returned:  " + success);