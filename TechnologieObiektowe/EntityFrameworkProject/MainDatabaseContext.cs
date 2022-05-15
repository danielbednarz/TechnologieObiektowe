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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<TechnicalWorker> TechnicalWorkers { get; set; }
        public DbSet<Nurse> Nurses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = ConfigurationHelper.GetConfiguration();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("MainDatabaseContext"));

            optionsBuilder.UseSqlServer("Server=OMEN-15\\SQLINSTANCE;Database=EFDatabase;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<TechnicalWorker>().ToTable("TechnicalWorkers");
            modelBuilder.Entity<Nurse>().ToTable("Nurses");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
        }
    }
}
