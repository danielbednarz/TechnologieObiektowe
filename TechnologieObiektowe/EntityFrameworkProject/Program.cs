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

            using var context = new MainDatabaseContext();

            TestQueries(context);

            Console.WriteLine("\nNaciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

        private static void TestQueries(MainDatabaseContext context)
        {
            Console.WriteLine("\n\nZapytanie 1.");
            var select1ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select1(context));
            Logger.WriteLog($"Czas wykonania pierwszego selecta: {select1ElapsedTime}");

            Console.WriteLine("\n\nZapytanie 2.");
            var select2ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select2(context));
            Logger.WriteLog($"Czas wykonania drugiego selecta: {select2ElapsedTime}");

            Console.WriteLine("\n\nZapytanie 3.");
            var select3ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select3(context));
            Logger.WriteLog($"Czas wykonania trzeciego selecta: {select3ElapsedTime}");

            Console.WriteLine("\n\nZapytanie 4.");
            var select4ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select4(context));
            Logger.WriteLog($"Czas wykonania czwartego selecta: {select4ElapsedTime}");
        }

    }
}