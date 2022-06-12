using DataGenerator;
using NHibernateProject.Model;

namespace NHibernateProject
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

        public static void Seed(MainDatabaseContext context)
        {
            List<Medicament> medicaments = AddMedicaments(context);
            List<Department> departments = AddDepartments(context);
            AddNurses(context, departments);
            AddTechnicalWorkers(context, departments);
            List<Doctor> doctors = AddDoctors(context, departments);
            List<Patient> patients = AddPatients(context);
            List<Visit> visits = AddVisits(context, doctors, patients);
            List<Recipe> recipes = AddRecipes(context, visits);
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
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Medicaments, OperationType.AddRange, addRangeElapsedTime);

            return medicaments;
        }

        private static List<Department> AddDepartments(MainDatabaseContext context)
        {
            if (context.Departments.Any())
            {
                return null;
            }

            var departmentsVM = DepartmentsGenerator.GenerateDepartments();

            List<Department> departments = departmentsVM.Select(x => new Department()
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(departments));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Departments, OperationType.AddRange, addRangeElapsedTime);

            return departments;
        }

        private static void AddNurses(MainDatabaseContext context, List<Department> departments)
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
                Department = departments.FirstOrDefault(y => y.Id == x.DepartmentId)
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(nurses));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Nurses, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddTechnicalWorkers(MainDatabaseContext context, List<Department> departments)
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
                Department = departments.FirstOrDefault(y => y.Id == x.DepartmentId)
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(techicalWorkers));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.TechnicalWorkers, OperationType.AddRange, addRangeElapsedTime);
        }

        private static List<Doctor> AddDoctors(MainDatabaseContext context, List<Department> departments)
        {
            if (context.Doctors.Any())
            {
                return null;
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
                Department = departments.FirstOrDefault(y => y.Id == x.DepartmentId)
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(doctors));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Doctors, OperationType.AddRange, addRangeElapsedTime);

            // Pobierane jest ID pierwszego doktora
            return doctors;
        }

        private static List<Patient> AddPatients(MainDatabaseContext context)
        {
            if (context.Patients.Any())
            {
                return null;
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
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Patients, OperationType.AddRange, addRangeElapsedTime);

            return patients;
        }

        private static List<Visit> AddVisits(MainDatabaseContext context, List<Doctor> doctors, List<Patient> patients)
        {
            if (context.Visits.Any())
            {
                return null;
            }

            List<VisitVM> visitsVM = VisitsGenerator.GenerateVisits(visitCount, doctorsCount, patientsCount, doctors.FirstOrDefault().Id);

            List<Visit> visits = visitsVM.Select(x => new Visit
            {
                VisitDate = x.VisitDate,
                Diagnosis = x.Diagnosis,
                Description = x.Description,
                Cost = decimal.ToDouble(x.Cost),
                Patient = patients.FirstOrDefault(y => y.Id == x.PatientId),
                Doctor = doctors.FirstOrDefault(y => y.Id == x.DoctorId)
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(visits));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Visits, OperationType.AddRange, addRangeElapsedTime);

            return visits;
        }

        private static List<Recipe> AddRecipes(MainDatabaseContext context, List<Visit> visits)
        {
            if (context.Recipes.Any())
            {
                return null;
            }

            var recipesVM = RecipesGenerator.GenerateRecipes(recipesCount, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                Visit = visits.FirstOrDefault(y => y.Id == x.VisitId)
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(recipes));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.Recipes, OperationType.AddRange, addRangeElapsedTime);

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
                int randMedicament = rnd.Next(0, medicaments.Count - 1);
                RecipeMedicament recipeMedicament = new RecipeMedicament()
                {
                    Recipe = recipe,
                    Medicament = medicaments[randMedicament]
                };
                list.Add(recipeMedicament);
            }

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() => context.AddRange(list));
            Logger.WriteCsvLog(OrmType.NHibernate, TableType.RecipeMedicaments, OperationType.AddRange, addRangeElapsedTime);

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
