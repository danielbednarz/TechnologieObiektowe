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

    List<Medicament> medicaments = new();
    List<Recipe> recipes = new();
    List<RecipeMedicament> recipeMedicaments = new();
    medicaments.AddRange(new List<Medicament>
    {
        new Medicament { Name = "Ibuprom" },
        new Medicament { Name = "APAP" },
    });
    recipes.AddRange(new List<Recipe>
    {
        new Recipe { IssueDate = new DateTime(2018, 6, 1, 12, 32, 30) },
        new Recipe { IssueDate = new DateTime(2021, 4, 2, 16, 42, 00) },
        new Recipe { IssueDate = new DateTime(2022, 11, 6, 11, 25, 00) },
    });

    await context.AddRange(medicaments);
    await context.AddRange(recipes);

    await context.Commit();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    await context.Rollback();
}
finally
{
    context.CloseTransaction();
}

Console.WriteLine("Zakończono działanie");
