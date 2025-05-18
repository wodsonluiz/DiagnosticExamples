// See https://aka.ms/new-console-template for more information
using System;
using EventExamples.Library;

//var eventSourceListener = new MyEventSourceListener();
//var eventSourceListener = new MyEventSourceListener("Wod.MyExampleEventSource");

SampleEvents lib = new SampleEvents();
int result = lib.Calculate(2, 4);
Console.WriteLine("Calculation from SampleEvents library is {0}", result);

//string url = "http://localhost:5052/WeatherForecast";
//var client = new HttpClient();

//var response = client.GetAsync(url)
//    .GetAwaiter()
//    .GetResult();

//var success = response.Content.ReadAsStringAsync()
//    .GetAwaiter()
//    .GetResult();

//Console.WriteLine("Web request returned: {0}", success);
