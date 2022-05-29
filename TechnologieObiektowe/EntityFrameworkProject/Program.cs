using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EntityFrameworkProject
{
    public class Program
    {
        public static void Main()
        {
            var firstElapsedTime = StopwatchHelper.MeasureExecutionTime(DbInitializer.Seed);

            var secondElapsedTime = StopwatchHelper.MeasureExecutionTime(DbInitializer.Seed);

            Logger.WriteLog($"Czas stworzenia bazy oraz wypełnienia jej danymi: {firstElapsedTime}");
            Logger.WriteLog($"Czas sprawdzenia czy baza danych jest utworzona i wypełniona danymi: {secondElapsedTime}");

            Console.WriteLine("\nNaciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

    }
}