using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernateProject;
using NHibernateProject.Infrastructure;
using NHibernateProject.Model;

Console.WriteLine("Rozpoczynam działanie...");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => 
    { 
        services.AddNHibernate("Server=OMEN-15\\SQLINSTANCE;Database=NHDatabase;Trusted_Connection=True;Encrypt=False;");
    })
    .Build();

MainDatabaseContext context = (MainDatabaseContext)host.Services.GetService(typeof(MainDatabaseContext));

try
{
    context.BeginTransaction();

    Medicament medicament = new Medicament()
    {
        Name = "Ibuprom"
    };

    await context.Save(medicament);
    await context.Commit();
}
catch
{
    await context.Rollback();
}
finally
{
    context.CloseTransaction();
}

Console.WriteLine("Zakończono działanie");
