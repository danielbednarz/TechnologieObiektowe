using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernateProject;
using NHibernateProject.Infrastructure;
using NHibernateProject.Model;
using NHibernateProject.Tests;
using System.Diagnostics;

Console.WriteLine("Rozpoczynam działanie...");

//var host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddNHibernate("Server=OMEN-15\\SQLINSTANCE;Database=NHDatabase;Trusted_Connection=True;Encrypt=False;");
//    })
//    .Build();

using (var session = NHibernateExtensions.OpenSession("Server=OMEN-15\\SQLINSTANCE;Database=NHDatabase;Trusted_Connection=True;Encrypt=False;"))
{
    MainDatabaseContext context = new MainDatabaseContext(session);
    try
    {
        context.BeginTransaction();
        Stopwatch watch = new();
        watch.Start();
        var t1 = Task.Run(async () => { await DbInitializer.Seed(context); });
        await Task.Factory.ContinueWhenAll(new[] { t1 }, tasks => watch.Stop());
        string time = watch.Elapsed.ToString();
        Console.WriteLine($"Dodawanie danych trwalo {time}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        await context.Rollback();
    }
    finally 
    {
        context.CloseTransaction();
        Console.WriteLine("Zakończono działanie");
    }

    try
    {
        context.BeginTransaction();
        Query.SelectReceipts(context);
        Query.SelectDepartmentsVisitsCosts(context);
        Query.SelectCompaniesWithMedicamentsCount(context);
        Query.SelectPatientWithMostMedicines(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        await context.Rollback();
    }
    finally 
    {
        context.CloseTransaction();
        Console.WriteLine("Zakończono działanie");
    }
}
