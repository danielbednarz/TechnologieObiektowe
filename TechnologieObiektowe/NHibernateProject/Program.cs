using DataGenerator;
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

        var seedElapsedTime = StopwatchHelper.MeasureExecutionTime(() => DbInitializer.Seed(context));

        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Create, seedElapsedTime);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        await context.RollbackAsync();
    }
    finally 
    {
        context.CloseTransaction();
        Console.WriteLine("Zakończono działanie");
    }

    try
    {
        context.BeginTransaction();

        var selectReceiptsElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.SelectReceipts(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectReceiptsElapsedTime);

        var selectDepartmentsVisitsCostsElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.SelectDepartmentsVisitsCosts(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectDepartmentsVisitsCostsElapsedTime);

        var selectCompaniesWithMedicamentsCountElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.SelectCompaniesWithMedicamentsCount(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectCompaniesWithMedicamentsCountElapsedTime);

        var selectPatientWithMostMedicinesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.SelectPatientWithMostMedicines(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectPatientWithMostMedicinesElapsedTime);

        var selectDoctorsVisits = StopwatchHelper.MeasureExecutionTime(() => Query.SelectDoctorsVisits(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectDoctorsVisits);

        var selectMedicamentsTypesInFestYearHalf = StopwatchHelper.MeasureExecutionTime(() => Query.SelectMedicamentsTypesInFestYearHalf(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.MultipleTables, OperationType.Select, selectMedicamentsTypesInFestYearHalf);

        var updateVisitDescriptionsElapsedTime = StopwatchHelper.MeasureExecutionTime(() => Query.UpdateVisitDescriptions(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Visits, OperationType.Update, updateVisitDescriptionsElapsedTime);

        var updateDoctorsSalaryByDepartmentByVisitCount = StopwatchHelper.MeasureExecutionTime(() => Query.UpdateDoctorsSalaryByDepartmentByVisitCount(context));
        Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Visits, OperationType.Update, updateDoctorsSalaryByDepartmentByVisitCount);

        context.Commit();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        await context.RollbackAsync();
    }
    finally 
    {
        context.CloseTransaction();
        Console.WriteLine("Zakończono działanie");
    }
}
