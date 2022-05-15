using DataGenerator;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject
{
    public class DbInitializer
    {
        public static void Seed(MainDatabaseContext context)
        {
            context.Database.Migrate();

            AddMedicaments(context);
            AddRecipes(context);
            AddDepartments(context);
            AddNurses(context);
            AddTechnicalWorkers(context);
        }

        private static void AddMedicaments(MainDatabaseContext context)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(30);

            if (context.Medicaments.Any())
            {
                return;
            }

            List<Medicament> medicaments = medicamentsVM.Select(x => new Medicament()
            {
                Name = x.Name,
                Type = x.Type,
                Company = x.Company
            }).ToList();

            context.Medicaments.AddRange(medicaments);
            context.SaveChanges();
        }

        private static void AddRecipes(MainDatabaseContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            List<Recipe> recipes = new()
            {
                new Recipe
                {
                    IssueDate = DateTime.Now,
                    Medicaments = context.Medicaments.Where(x => x.Name == "APAP").ToList()
                }
            };

            context.Recipes.AddRange(recipes);
            context.SaveChanges();
        }

        private static void AddDepartments(MainDatabaseContext context)
        {
            if(context.Departments.Any())
            {
                return;
            }

            var departmentsVM = DepartmentsGenerator.GenerateDepartments();

            List<Department> departments = departmentsVM.Select(x => new Department()
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            context.Departments.AddRange(departments);
            context.SaveChanges();
        }

        private static void AddNurses(MainDatabaseContext context)
        {
            if (context.Nurses.Any())
            {
                return;
            }

            var nursesVM = NursesGenerator.GenerateNurses(30);
            var nurses = nursesVM.Select(x => new Nurse
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Role = x.Role,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Salary = x.Salary,
                DepartmentId = x.DepartmentId
            });

            context.AddRange(nurses);
            context.SaveChanges();
        }

        private static void AddTechnicalWorkers(MainDatabaseContext context)
        {
            if (context.TechnicalWorkers.Any())
            {
                return;
            }

            var technicalWorkersVM = TechnicalWorkersGenerator.GenerateTechnicalWorkers(30);

            var technicalWorkers = technicalWorkersVM.Select(x => new TechnicalWorker
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Role = x.Role,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Salary = x.Salary,
                DepartmentId = x.DepartmentId
            });

            context.TechnicalWorkers.AddRange(technicalWorkers);
            context.SaveChanges();
        }
    }
}
