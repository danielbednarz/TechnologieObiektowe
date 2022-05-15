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
        }

        private static void AddMedicaments(MainDatabaseContext context)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(30);

            if (context.Medicaments.Any())
            {
                return;
            }

            //List<Medicament> medicaments = new()
            //{
            //    new Medicament
            //    {
            //        Name = "Ibuprom",
            //        Company = "Polfarmex",
            //        Type = "Tabletka powlekana"

            //    },
            //    new Medicament
            //    {
            //        Name = "Apap",
            //        Company = "USP ZDROWIE",
            //        Type = "Tabletka powlekana"
            //    },
            //    new Medicament
            //    {
            //        Name = "Acard",
            //        Company = "Acard",
            //        Type = "Tabletka powlekana"
            //    },
            //};

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

            Department department = new()
            {
                Name = "Wydział Kardiologiczny",
                PhoneNumber = 450
            };

            context.Departments.Add(department);
            context.SaveChanges();
        }

        private static void AddNurses(MainDatabaseContext context)
        {
            if (context.Nurses.Any())
            {
                return;
            }

            Nurse nurse = new()
            {
                Name = "Jan",
                Surname = "Kowalski",
                Gender = 0,
                Salary = 2400,
                Address = "Kielce, ul. Wojska Polskiego",
                BirthDate = DateTime.Now,
                DepartmentId = context.Departments.FirstOrDefault(x => x.PhoneNumber == 450).Id,
                Role = "Ciężkie prace"
            };

            context.Nurses.Add(nurse);
            context.SaveChanges();
        }
    }
}
