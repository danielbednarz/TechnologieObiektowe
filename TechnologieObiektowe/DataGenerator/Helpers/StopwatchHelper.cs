using System.Diagnostics;

namespace DataGenerator
{
    public class StopwatchHelper
    {
        public static TimeSpan MeasureExecutionTime(Action method)
        {
            Stopwatch stopwatch = new();

            stopwatch.Start();
            method();
            stopwatch.Stop();
            
            var elapsedTime = stopwatch.Elapsed;
            
            stopwatch.Reset();

            return elapsedTime;
        }
    }
}
