using DataGenerator;
using NHibernateProject.Model;

namespace NHibernateProject
{
    public class DbInitializer
    {
        //private const int medicamentCount = 2000;
        //private const int visitCount = 100000;
        //private const int recipesCount = 150000;
        //private const int doctorsCount = 5000;
        //private const int patientsCount = 40000;
        //private const int nursesCount = 10000;
        //private const int technicalWorkersCount = 500;

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

        public static void Seed(MainDatabaseContext context)
        {
            List<Medicament> medicaments = AddMedicaments(context);
            AddDepartments(context);
            AddNurses(context);
            AddTechnicalWorkers(context);
            int firstDoctorId = AddDoctors(context);
            AddPatients(context);
            AddVisits(context, firstDoctorId);
            List<Recipe> recipes = AddRecipes(context);
            AddRecipeMedicaments(context, recipes, medicaments);

            context.Commit();
        }

        private static List<Medicament> AddMedicaments(MainDatabaseContext context)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(medicamentCount);

            if (context.Medicaments.Any())
            {
                return null;
            }

            List<Medicament> medicaments = medicamentsVM.Select(x => new Medicament()
            {
                Name = x.Name,
                Type = x.Type,
                Company = x.Company
            }).ToList();


            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(medicaments));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Medicaments, OperationType.AddRange, addRangeElapsedTime);

            return medicaments;
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(departments));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Departments, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddNurses(MainDatabaseContext context)
        {
            if (context.Nurses.Any())
            {
                return;
            }

            var nursesVM = NursesGenerator.GenerateNurses(nursesCount);

            List<Nurse> nurses = nursesVM.Select(x => new Nurse()
            {
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Address = x.Address,
                Salary = decimal.ToDouble(x.Salary),
                Role = x.Role,
                DepartmentId = x.DepartmentId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(nurses));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Nurses, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddTechnicalWorkers(MainDatabaseContext context)
        {
            if (context.TechnicalWorkers.Any())
            {
                return;
            }

            var techicalWorkersVM = TechnicalWorkersGenerator.GenerateTechnicalWorkers(technicalWorkersCount);

            List<TechnicalWorker> techicalWorkers = techicalWorkersVM.Select(x => new TechnicalWorker()
            {
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Address = x.Address,
                Salary = decimal.ToDouble(x.Salary),
                Role = x.Role,
                DepartmentId = x.DepartmentId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(techicalWorkers));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.TechnicalWorkers, OperationType.AddRange, addRangeElapsedTime);
        }

        private static int AddDoctors(MainDatabaseContext context)
        {
            if (context.Doctors.Any())
            {
                return context.Doctors.First().Id;
            }

            List<DoctorVM> doctorsVM = DoctorsGenerator.GenerateDoctors(doctorsCount);

            List<Doctor> doctors = doctorsVM.Select(x => new Doctor
            {
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                Specialization = x.Specialization,
                Address = x.Address,
                BirthDate = x.BirthDate,
                Salary = decimal.ToDouble(x.Salary),
                DepartmentId = x.DepartmentId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(doctors));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Doctors, OperationType.AddRange, addRangeElapsedTime);

            // Pobierane jest ID pierwszego doktora
            return doctors.First().Id;
        }

        private static void AddPatients(MainDatabaseContext context)
        {
            if (context.Patients.Any())
            {
                return;
            }

            var patientsVM = PatientsGenerator.GeneratePatients(patientsCount);

            List<Patient> patients = patientsVM.Select(x => new Patient()
            {
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Address = x.Address
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(patients));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Patients, OperationType.AddRange, addRangeElapsedTime);
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
                Cost = decimal.ToDouble(x.Cost),
                PatientId = x.PatientId,
                DoctorId = x.DoctorId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(visits));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Visits, OperationType.AddRange, addRangeElapsedTime);
        }

        private static List<Recipe> AddRecipes(MainDatabaseContext context)
        {
            if (context.Recipes.Any())
            {
                return null;
            }

            var recipesVM = RecipesGenerator.GenerateRecipes(recipesCount, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                VisitId = x.VisitId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(recipes));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Recipes, OperationType.AddRange, addRangeElapsedTime);

            return recipes;
        }

        private static void AddRecipeMedicaments(MainDatabaseContext context, List<Recipe> recipes, List<Medicament> medicaments)
        {
            if (context.RecipeMedicaments.Any())
            {
                return;
            }

            List<RecipeMedicament> list = new();

            Random rnd = new Random();
            foreach (var recipe in recipes)
            {
                RecipeMedicament recipeMedicament = new RecipeMedicament()
                {
                    RecipeId = recipe.Id,
                    MedicamentId = medicaments[rnd.Next(1, medicaments.Count)].Id,
                };
                list.Add(recipeMedicament);
            }

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(list));
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.RecipeMedicaments, OperationType.AddRange, addRangeElapsedTime);

            //List<Recipe> recipes = context.Recipes.Where(x => x.Id != null).ToList();
            //foreach (var recipe in recipes)
            //{
            //    Medicament medicament = context.Medicaments.FirstOrDefault();
            //    recipe.RecipeMedicaments.Add(medicament);
            //    await context.AddMedicamentToRecipe(recipe);
            //}
        }
    }
}
