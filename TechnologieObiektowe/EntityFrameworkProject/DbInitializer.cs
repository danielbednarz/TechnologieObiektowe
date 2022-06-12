using DataGenerator;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject
{
    public class DbInitializer
    {
        //private const int medicamentCount = 2000;
        //private const int visitCount = 100000;
        //private const int recipeCount = 150000;
        //private const int doctorCount = 5000;
        //private const int patientCount = 40000;
        //private const int nurseCount = 10000;
        //private const int technicalWorkerCount = 500;

        //private const int medicamentCount = 200;
        //private const int visitCount = 1000;
        //private const int recipesCount = 1500;
        //private const int doctorsCount = 50;
        //private const int patientsCount = 400;
        //private const int nursesCount = 100;
        //private const int technicalWorkersCount = 50;

        //private const int medicamentCount = 500;
        //private const int visitCount = 20000;
        //private const int recipesCount = 30000;
        //private const int doctorsCount = 300;
        //private const int patientsCount = 8000;
        //private const int nursesCount = 600;
        //private const int technicalWorkersCount = 200;

        private const int medicamentCount = 500;
        private const int visitCount = 200000;
        private const int recipesCount = 300000;
        private const int doctorsCount = 300;
        private const int patientsCount = 8000;
        private const int nursesCount = 600;
        private const int technicalWorkersCount = 200;

        public static void Seed()
        {
            using var context = new MainDatabaseContext();

            context.Database.Migrate();

            AddMedicaments(context);
            AddDepartments(context);
            int firstDoctorId = AddEmployees(context);
            AddPatients(context);
            AddVisits(context, firstDoctorId);
            AddRecipes(context);
            AddMedicamentRecipes(context);
        }


        private static void AddMedicaments(MainDatabaseContext context)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(medicamentCount);

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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Medicaments, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Departments, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static int AddEmployees(MainDatabaseContext context)
        {
            if (context.Employees.Any())
            {
                return context.Employees.First().Id;
            }

            AddNurses(context);
            AddTechnicalWorkers(context);
            var doctorId = AddDoctors(context);

            return doctorId;
        }

        private static void AddNurses(MainDatabaseContext context)
        {
            var nursesVM = NursesGenerator.GenerateNurses(nursesCount);
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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Nurses, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static void AddTechnicalWorkers(MainDatabaseContext context)
        {
            var technicalWorkersVM = TechnicalWorkersGenerator.GenerateTechnicalWorkers(technicalWorkersCount);

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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.TechnicalWorkers, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static int AddDoctors(MainDatabaseContext context)
        {
            List<DoctorVM> doctorsVM = DoctorsGenerator.GenerateDoctors(doctorsCount);

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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Doctors, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);

            // Pobierane jest ID pierwszego doktora
            return doctors.First().Id;
        }

        private static void AddPatients(MainDatabaseContext context)
        {
            if (context.Patients.Any())
            {
                return;
            }

            List<PatientVM> patientsVM = PatientsGenerator.GeneratePatients(patientsCount);

            List<Patient> patients = patientsVM.Select(x => new Patient
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Address = x.Address,
                BirthDate = x.BirthDate,
            }).ToList();


            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Patients.AddRange(patients));
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Patients, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static void AddVisits(MainDatabaseContext context, int firstDoctorId)
        {
            if (context.Visits.Any())
            {
                return;
            }

            List<VisitVM> visitsVM = VisitsGenerator.GenerateVisits(visitCount, doctorsCount, patientsCount, firstDoctorId);

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
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Visits, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static void AddRecipes(MainDatabaseContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            List<RecipeVM> recipesVM = RecipesGenerator.GenerateRecipes(recipesCount, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                VisitId = x.VisitId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.Recipes.AddRange(recipes));
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.Recipes, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

        private static void AddMedicamentRecipes(MainDatabaseContext context)
        {
            if (context.RecipeMedicaments.Any())
            {
                return;
            }

            var medicaments = context.Medicaments.ToList();
            var recipes = context.Recipes.ToList();

            Random random = new();

            List<RecipeMedicament> recipeMedicamets = new();

            foreach (var recipe in recipes)
            {
                RecipeMedicament recipeMedicament = new()
                {
                    MedicamentId = medicaments[random.Next(1, medicaments.Count)].Id,
                    RecipeId = recipe.Id
                };

                recipeMedicamets.Add(recipeMedicament);
            }

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.RecipeMedicaments.AddRange(recipeMedicamets));
            var saveChangesElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.SaveChanges());

            Logger.WriteCsvLog(OrmType.EntityFramework, TableType.RecipeMedicaments, OperationType.AddRange, addRangeElapsedTime + saveChangesElapsedTime);
        }

    }
}
