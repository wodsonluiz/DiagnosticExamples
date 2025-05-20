using System;
using System.Diagnostics.Tracing;

namespace EventExamples
{
    public class MyEventSourceListener : EventListener
    {
        private readonly string _eventSourceName;

        public MyEventSourceListener()
        {

        }

        public MyEventSourceListener(string name)
        {
            _eventSourceName = name;
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);

            Console.WriteLine("New Event source: {0}", eventSource.Name);

            if (eventSource.Name == _eventSourceName)
            {
                Console.WriteLine("Enabling events for: {0}", eventSource.Name);

                EnableEvents(eventSource, EventLevel.LogAlways, EventKeywords.All);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            base.OnEventWritten(eventData);

            Console.WriteLine("EventSource Name: {0}", eventData.EventSource.Name);
            Console.WriteLine("Event Name: {0}", eventData.EventName);
            Console.WriteLine("EventSource Id: {0}", eventData.EventId);
            Console.WriteLine("EventSource Level: {0}", eventData.Level);

            foreach (var pl in eventData.Payload)
            {
                Console.WriteLine("Payload: {0}", pl.ToString());
            }
            
            Console.WriteLine("Message: {0}", eventData.Message);
            Console.WriteLine("---------------------------------------");
        }
    }
}