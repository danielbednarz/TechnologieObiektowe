using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkProject
{
    public class Program
    {
        public static void Main()
        {
            using var context = new MainDatabaseContext();

            Seed(context);

            Console.WriteLine("Naciśnij klawisz, by zakończyc...");
            Console.ReadLine();
        }

        private static void Seed(MainDatabaseContext context)
        {
            context.Database.Migrate();

            AddMedicaments(context);
            AddRecipes(context);
        }

        private static void AddMedicaments(MainDatabaseContext context)
        {
            if (context.Medicaments.Any())
            {
                return;
            }

            List<Medicament> medicaments = new()
            {
                new Medicament
                {
                    Name = "Ibuprom",
                    Company = "Polfarmex",
                    Type = "Tabletka powlekana"

                },
                new Medicament
                {
                    Name = "Apap",
                    Company = "USP ZDROWIE",
                    Type = "Tabletka powlekana"
                },
                new Medicament
                {
                    Name = "Acard",
                    Company = "Acard",
                    Type = "Tabletka powlekana"
                },
            };

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

    }
}