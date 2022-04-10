using EntityFrameworkProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkProject
{

    public class MainDatabaseContext : DbContext
    {

        //Add-Migration -Context MainDatabaseContext -o Migrations/MainDatabaseMigrations <Nazwa migracji>
        //Update-Database -Context MainDatabaseContext
        //Remove-Migration -Context MainDatabaseContext
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update

        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = ConfigurationHelper.GetConfiguration();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("MainDatabaseContext"));

            optionsBuilder.UseSqlServer("Server=DESKTOP-CP4DUVM;Database=EFDatabase;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
