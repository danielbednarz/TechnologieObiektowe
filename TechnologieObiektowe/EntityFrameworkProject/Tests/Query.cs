using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject
{
    public class Query
    {
        public static void Select1(MainDatabaseContext context)
        {
            var query = context.MedicamentRecipes.Include(x => x.Medicament)
                .Include(x => x.Recipe)
                    .ThenInclude(y => y.Visit)
                    .ThenInclude(z => z.Patient)
                .Where(x =>
                    x.Recipe.IssueDate < new DateTime(2015, 1, 1)
                    && x.Medicament.Type == "tabletki"
                    && x.Recipe.Visit.Cost > 300
                    && x.Recipe.Visit.Patient.Gender == 0)
                .GroupBy(x => new { x.Medicament.Name, Type = x.Medicament.Type, Company = x.Medicament.Company })
                .Select(g => new
                {
                    Name = g.Key,
                    Company = g.Key.Company,
                    Type = g.Key.Type,
                    MedicamentCount = g.Count()
                }).ToList();

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} {item.Type}, {item.Company} => {item.MedicamentCount}");
            }

            Console.WriteLine("\n\n");
        }

        public static void Select2(MainDatabaseContext context)
        {
            var query = context.Visits.Include(x => x.Doctor)
                .ThenInclude(y => y.Department)
                .Where(x => x.Diagnosis.Length > 10)
                .GroupBy(x => new { x.Doctor.Department.Name })
                .Select(g => new
                {
                    Name = g.Key,
                    VisitTotalCost = g.Sum(x => x.Cost)
                }).OrderByDescending(x => x.VisitTotalCost).ToList();

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} => {item.VisitTotalCost}");
            }

            Console.WriteLine("\n\n");
        }
         
        public static void Select3(MainDatabaseContext context)
        {
            var query = context.MedicamentRecipes.Include(x => x.Medicament)
                .Include(x => x.Recipe)
                    .ThenInclude(y => y.Visit)
                .GroupBy(x => new { Company = x.Medicament.Company, Name = x.Medicament.Name })
                .Select(g => new
                {
                    Company = g.Key.Company,
                    Name = g.Key.Name,
                    PrescribedMedicamentsCount = g.Count()
                }).OrderByDescending(x => x.PrescribedMedicamentsCount).ToList();

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Company} - {item.Name} => {item.PrescribedMedicamentsCount}");
            }

            Console.WriteLine("\n\n");
        }

        public static void Select4(MainDatabaseContext context)
        {
            var query = context.MedicamentRecipes
                .Include(x => x.Recipe)
                    .ThenInclude(y => y.Visit)
                    .ThenInclude(z => z.Patient)
                .GroupBy(x => new { PatientId = x.Recipe.Visit.Patient.Id, PatientName = x.Recipe.Visit.Patient.Name, PatientSurname = x.Recipe.Visit.Patient.Surname })
                .Select(x => new
                {
                    PatientId = x.Key.PatientId,
                    PatientName = x.Key.PatientName,
                    PatientSurname = x.Key.PatientSurname,
                    MedicamentsCount = x.Count()
                })
                .OrderByDescending(x => x.MedicamentsCount)
                .First();

            Console.WriteLine($"Pacjent z największą przepisaną liczbą leków to {query.PatientName} {query.PatientSurname} ({query.MedicamentsCount}).");

            Console.WriteLine("\n\n");
        }
    }
}
