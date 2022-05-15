using DataGenerator;
using NHibernateProject.Model;

namespace NHibernateProject
{
    public class DbInitializer
    {
        public static async Task Seed(MainDatabaseContext context)
        {
            await AddMedicaments(context);
            await AddRecipes(context);
            await AddDepartments(context);
            await context.Commit();
        }

        private static async Task AddMedicaments(MainDatabaseContext context)
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

            await context.AddRange(medicaments);
        }

        private static async Task AddDepartments(MainDatabaseContext context)
        {
            if (context.Departments.Any())
            {
                return;
            }

            var departmentsVM = DepartmentsGenerator.GenerateDepartments();

            List<Department> departments = departmentsVM.Select(x => new Department()
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            await context.AddRange(departments);
        }

        private static async Task AddRecipes(MainDatabaseContext context)
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

            await context.AddRange(recipes);
        }
    }
}
