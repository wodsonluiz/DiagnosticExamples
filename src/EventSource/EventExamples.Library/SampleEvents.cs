namespace EventExamples.Library
{
    public class SampleEvents
    {
        public SampleEvents()
        {
            MyExampleEventSource.Log.Startup();
        }

        public int Calculate(int num1, int num2)
        {
            MyExampleEventSource.Log.PerformCalculation(num1, num2);

            int total = num1 + num2;

            MyExampleEventSource.Log.CalculationComplete();
            MyExampleEventSource.Log.WarningMessage("An example warning message form inside the code");

            return total;
        }
    }
}