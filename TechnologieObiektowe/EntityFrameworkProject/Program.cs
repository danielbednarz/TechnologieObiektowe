using DataGenerator;

namespace EntityFrameworkProject
{
    public class Program
    {
        public static void Main()
        {
            var seedElapsedTime = StopwatchHelper.MeasureExecutionTime(DbInitializer.Seed);

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Create, seedElapsedTime);

            using var context = new MainDatabaseContext();

            TestQueries(context);

            Console.WriteLine("\nNaciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

        private static void TestQueries(MainDatabaseContext context)
        {
            Console.WriteLine("\n\nSelect - Zapytanie 1.");
            var select1ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select1(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Select, select1ElapsedTime);

            Console.WriteLine("\n\nZapytanie 2.");
            var select2ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select2(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Select, select2ElapsedTime);

            Console.WriteLine("\n\nZapytanie 3.");
            var select3ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select3(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Select, select3ElapsedTime);

            Console.WriteLine("\n\nZapytanie 4.");
            var select4ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select4(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Select, select4ElapsedTime);

            Console.WriteLine("\n\nZapytanie 5.");
            var select5ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Select5(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.MultipleTables, OperationType.Select, select5ElapsedTime);

            var update1ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Update1(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Visits, OperationType.Update, update1ElapsedTime);

            var update2ElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.Update2(context));
            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Doctors, OperationType.Update, update2ElapsedTime);
        }

    }
}