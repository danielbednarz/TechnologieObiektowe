using DataGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace EntityFrameworkProject
{
    public class DbInitializer
    {
        const int visitCount = 10000;
        const int doctorCount = 40;
        const int patientCount = 1000;

        public static void Seed()
        {
            using var context = new MainDatabaseContext();

            context.Database.Migrate();

            AddMedicaments(context);
            AddDepartments(context);
            AddNurses(context);
            AddTechnicalWorkers(context);
            int firstDoctorId = AddDoctors(context);
            AddPatients(context);
            AddVisits(context, firstDoctorId);
            AddRecipes(context);
            AddMedicamentRecipes(context);
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Medicaments.AddRange(medicaments));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Medicaments: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Medicaments");
        }


        private static void AddDepartments(MainDatabaseContext context)
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Departments.AddRange(departments));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Departments: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Departments");
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Nurses.AddRange(nurses));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Nurses: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Nurses");
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.TechnicalWorkers.AddRange(technicalWorkers));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli TechnicalWorkers: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "TechnicalWorkers");
        }

        private static int AddDoctors(MainDatabaseContext context)
        {
            if (context.Doctors.Any())
            {
                return context.Doctors.First().Id;
            }

            List<DoctorVM> doctorsVM = DoctorsGenerator.GenerateDoctors(doctorCount);

            List<Doctor> doctors = doctorsVM.Select(x => new Doctor
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Specialization = x.Specialization,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Salary = x.Salary,
                DepartmentId = x.DepartmentId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Doctors.AddRange(doctors));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Doctors: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Doctors");

            // Pobierane jest ID pierwszego doktora
            return doctors.First().Id;
        }

        private static void AddPatients(MainDatabaseContext context)
        {
            if (context.Patients.Any())
            {
                return;
            }

            List<PatientVM> patientsVM = PatientsGenerator.GeneratePatients(patientCount);

            List<Patient> patients = patientsVM.Select(x => new Patient
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Address = x.Address,
                BirthDate = x.BirthDate,
            }).ToList();


            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Patients.AddRange(patients));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Patients: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Patients");
        }

        private static void AddVisits(MainDatabaseContext context, int firstDoctorId)
        {
            if (context.Visits.Any())
            {
                return;
            }

            List<VisitVM> visitsVM = VisitsGenerator.GenerateVisits(visitCount, doctorCount, patientCount, firstDoctorId);

            List<Visit> visits = visitsVM.Select(x => new Visit
            {
                VisitDate = x.VisitDate,
                Diagnosis = x.Diagnosis,
                Description = x.Description,
                Cost = x.Cost,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId
            }).ToList();

            var visitsToUpdate = context.Visits.Where(x => x.VisitDate > DateTime.Now).ToList();

            foreach (var visit in visitsToUpdate)
            {
                visit.Cost = 0;
            }

            context.Visits.UpdateRange(visitsToUpdate);
            context.SaveChanges();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Visits.AddRange(visits));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Visits: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Visits");
        }

        private static void AddRecipes(MainDatabaseContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            List<RecipeVM> recipesVM = RecipesGenerator.GenerateRecipes(8000, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                VisitId = x.VisitId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Recipes.AddRange(recipes));
            Logger.WriteLog($"Czas wykonania AddRange() dla tabeli Recipes: {addRangeElapsedTime}");

            SaveChangesAndLogTime(context, "Recipes");
        }

        private static void AddMedicamentRecipes(MainDatabaseContext context)
        {
            if (context.MedicamentRecipes.Any())
            {
                return;
            }

            var medicaments = context.Medicaments.ToList();
            var recipes = context.Recipes.ToList();

            Random random = new();

            foreach (var recipe in recipes)
            {
                MedicamentRecipe medicamentRecipe = new()
                {
                    MedicamentId = medicaments[random.Next(1, medicaments.Count)].Id,
                    RecipeId = recipe.Id
                };

                context.MedicamentRecipes.Add(medicamentRecipe);
            }

            SaveChangesAndLogTime(context, "MedicamentRecipes");
        }

        private static void SaveChangesAndLogTime(MainDatabaseContext context, string tableName)
        {
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());
            Logger.WriteLog($"Czas wykonania SaveChanges() dla tabeli {tableName}: {saveChangesElapsedTime}");
        }
    }
}
