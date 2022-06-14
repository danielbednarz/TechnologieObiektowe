using NHibernate;
using NHibernate.Linq;
using NHibernateProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateProject.Tests
{
    public static class Query
    {
        public static void SelectReceipts(MainDatabaseContext context)
        {
            var list = context.RecipeMedicaments
                .Fetch(x => x.Medicament)
                .Fetch(x => x.Recipe)
                    .ThenFetch(y => y.Visit)
                        .ThenFetch(z => z.Patient)
                .Where(x =>
                    x.Recipe.IssueDate < new DateTime(2015, 1, 1) &&
                    x.Medicament.Type == "tabletki" &&
                    x.Recipe.Visit.Cost > 300 &&
                    x.Recipe.Visit.Patient.Gender == 0)
                .GroupBy(x => new { Name = x.Medicament.Name, Type = x.Medicament.Type, Company = x.Medicament.Company })
                .Select(x => new
                {
                    Name = x.Key.Name,
                    Type = x.Key.Type,
                    Company = x.Key.Company,
                    MedicamentCount = x.Count()
                })
                .ToList();

            foreach (var el in list)
            {
                Console.WriteLine($"{el.Name} znalazł się na {el.MedicamentCount} receptach.");
            }

            Console.WriteLine("\n\n");
        }

        public static void SelectDepartmentsVisitsCosts(MainDatabaseContext context)
        {
            var list = context.Visits
                .Fetch(x => x.Doctor)
                    .ThenFetch(y => y.Department)
                .Where(x => x.Diagnosis.Length > 10)
                .GroupBy(x => new { DepartmentName = x.Doctor.Department.Name })
                .Select(x => new
                {
                    DepartmentName = x.Key.DepartmentName,
                    TotalCost = x.Sum(y => y.Cost)
                })
                .OrderByDescending(x => x.TotalCost)
                .ToList();

            foreach (var el in list)
            {
                Console.WriteLine($"{el.DepartmentName} otrzymał z wizyt kwotę równą {el.TotalCost}.");
            }

            Console.WriteLine("\n\n");
        }

        public static void SelectCompaniesWithMedicamentsCount(MainDatabaseContext context)
        {
            var list = context.RecipeMedicaments
                .Fetch(x => x.Medicament)
                .Fetch(x => x.Recipe)
                    .ThenFetch(y => y.Visit)
                .GroupBy(x => new { Company = x.Medicament.Company, Name = x.Medicament.Name })
                .Select(g => new
                {
                    Company = g.Key.Company,
                    Name = g.Key.Name,
                    PrescribedMedicamentsCount = g.Count()
                })
                .OrderByDescending(x => x.PrescribedMedicamentsCount)
                .ToList();

            foreach (var el in list)
            {
                Console.WriteLine($"Przepisano {el.PrescribedMedicamentsCount} leku {el.Name} z firmy {el.Company}.");
            }

            Console.WriteLine("\n\n");
        }

        public static void SelectPatientWithMostMedicines(MainDatabaseContext context)
        {
            var patient = context.RecipeMedicaments
                .Fetch(x => x.Recipe)
                    .ThenFetch(y => y.Visit)
                        .ThenFetch(z => z.Patient)
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

            Console.WriteLine($"Pacjent z największą przepisaną liczbą leków to {patient.PatientName} {patient.PatientSurname} ({patient.MedicamentsCount}).");

            Console.WriteLine("\n\n");
        }

        public static void SelectDoctorsVisits(MainDatabaseContext context)
        {
            var query = context.Visits
                .Fetch(x => x.Doctor)
                    .ThenFetch(x => x.Department)
                .GroupBy(x => new { x.VisitDate.Year, DepartmentName = x.Doctor.Department.Name, x.Doctor.Surname, x.Doctor.Specialization })
                .Select(x => new
                {
                    x.Key.Year,
                    x.Key.DepartmentName,
                    x.Key.Surname,
                    x.Key.Specialization,
                    VisitsPerDeparment = x.Count()
                })
                .OrderByDescending(x => x.VisitsPerDeparment).ToList();

            foreach (var item in query)
            {
                Console.WriteLine($"W roku {item.Year}, w oddziale {item.DepartmentName} lekarz {item.Surname} o specjalizacji {item.Specialization} odbył {item.VisitsPerDeparment} wizyt.");
            }

            Console.WriteLine("\n\n");
        }

        public static void SelectMedicamentsTypesInFestYearHalf(MainDatabaseContext context)
        {
            var types = context.RecipeMedicaments
                .Fetch(x => x.Medicament)
                .Fetch(x => x.Recipe)
                    .ThenFetch(x => x.Visit)
                        .ThenFetch(x => x.Doctor)
                .Fetch(x => x.Recipe)
                    .ThenFetch(x => x.Visit)
                        .ThenFetch(x => x.Patient)
                .Where(x => x.Recipe.IssueDate.Month <= 6 && x.Recipe.Visit.VisitDate.Month <= 6 && x.Recipe.Visit.Patient.BirthDate <= DateTime.Today.AddYears(-30))
                .GroupBy(x => new { MedicamentType = x.Medicament.Type, Year = x.Recipe.Visit.VisitDate.Year })
                .Select(x => new
                {
                    MedicamentType = x.Key.MedicamentType,
                    Year = x.Key.Year,
                    MedicamentTypeCount = x.Count()
                })
                .OrderByDescending(x => x.MedicamentTypeCount)
                .Take(5)
                .ToList();

            int counter = 0;
            Console.WriteLine("5 najczęściej wypisywanych na receptach typów leków w pierwszym kwartale danego roku (wizyta + recepta), gdy pacjent był starszy niż 30 lat:");
            foreach (var x in types)
            {
                Console.WriteLine($"#{++counter}: {x.MedicamentType} w roku {x.Year} w ilości {x.MedicamentTypeCount} opakowań.");
            }

            Console.WriteLine("\n\n");
        }

        public static void UpdateVisitDescriptions(MainDatabaseContext context)
        {
            var recipeMedicaments = context.RecipeMedicaments
                .Where(x => x.Medicament.Company == "Merck" && (x.Medicament.Type == "proszek" || x.Medicament.Type == "zastrzyk"))
                .Fetch(x => x.Medicament)
                .Fetch(x => x.Recipe)
                    .ThenFetch(y => y.Visit)
                .Select(x => new { RecipeMedicament = x, Recipe = x.Recipe, Visit = x.Recipe.Visit })
                .ToList();

            foreach (var record in recipeMedicaments)
            {
                record.Visit.Description = "Leki wycofane";
                context.Update(record.Visit);
            }
        }

        public static void UpdateDoctorsSalaryByDepartmentByVisitCount(MainDatabaseContext context)
        {
            var doctors = context.Doctors
                .Fetch(x => x.Department)
                .Fetch(x => x.Visits)
                .Where(x => x.Department.Name.Like("%Chorób%") && x.Specialization == "Alergolog")
                .Select(x => new { Doctor = x, VisitsCount = x.Visits.Count })
                .ToList();

            foreach (var record in doctors)
            {
                record.Doctor.Salary += record.VisitsCount * 20;
                context.Update(record.Doctor);
            }
        }
    }
}
