using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace EntityFrameworkProject
{
    public class Program
    {
        public static void Main()
        {
            using var context = new MainDatabaseContext();

            Stopwatch stopwatch = new();


            stopwatch.Start();
            DbInitializer.Seed(context);
            stopwatch.Stop();

            Console.WriteLine($"Czas stworzenia bazy oraz wypełnienia jej danymi: {stopwatch.Elapsed}");
            Console.WriteLine("Naciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

    }
}