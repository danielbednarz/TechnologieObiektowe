using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkProject
{
    public class Program
    {
        public static void Main()
        {
            using var context = new MainDatabaseContext();

            DbInitializer.Seed(context);

            Console.WriteLine("Naciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

    }
}