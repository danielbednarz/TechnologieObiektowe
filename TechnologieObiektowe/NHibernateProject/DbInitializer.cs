using DataGenerator;
using NHibernate;
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

        private static ITransaction transaction;
        public static void Seed(IStatelessSession session, ITransaction _transaction)
        {
            transaction = _transaction;
            List<Medicament> medicaments = AddMedicaments(session);
            AddDepartments(session);
            AddNurses(session);
            AddTechnicalWorkers(session);
            int firstDoctorId = AddDoctors(session);
            AddPatients(session);
            AddVisits(session, firstDoctorId);
            List<Recipe> recipes = AddRecipes(session);
            AddRecipeMedicaments(session, recipes, medicaments);

            transaction.Commit();
        }

        private static List<Medicament> AddMedicaments(IStatelessSession session)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(medicamentCount);

            List<Medicament> medicaments = medicamentsVM.Select(x => new Medicament()
            {
                Name = x.Name,
                Type = x.Type,
                Company = x.Company
            }).ToList();


            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var medicament in medicaments)
                {
                    session.Insert(medicament);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Medicaments, OperationType.AddRange, addRangeElapsedTime);

            return medicaments;
        }

        private static void AddDepartments(IStatelessSession session)
        {
            var departmentsVM = DepartmentsGenerator.GenerateDepartments();

            List<Department> departments = departmentsVM.Select(x => new Department()
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var department in departments)
                {
                    session.Insert(department);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Departments, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddNurses(IStatelessSession session)
        {
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in nurses)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Nurses, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddTechnicalWorkers(IStatelessSession session)
        {
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in techicalWorkers)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.TechnicalWorkers, OperationType.AddRange, addRangeElapsedTime);
        }

        private static int AddDoctors(IStatelessSession session)
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
                Salary = decimal.ToDouble(x.Salary),
                DepartmentId = x.DepartmentId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in doctors)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Doctors, OperationType.AddRange, addRangeElapsedTime);

            // Pobierane jest ID pierwszego doktora
            return doctors.First().Id;
        }

        private static void AddPatients(IStatelessSession session)
        {
            var patientsVM = PatientsGenerator.GeneratePatients(patientsCount);

            List<Patient> patients = patientsVM.Select(x => new Patient()
            {
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Address = x.Address
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in patients)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Patients, OperationType.AddRange, addRangeElapsedTime);
        }

        private static void AddVisits(IStatelessSession session, int firstDoctorId)
        {
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in visits)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Visits, OperationType.AddRange, addRangeElapsedTime);
        }

        private static List<Recipe> AddRecipes(IStatelessSession session)
        {

            var recipesVM = RecipesGenerator.GenerateRecipes(recipesCount, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                VisitId = x.VisitId
            }).ToList();

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in recipes)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.Recipes, OperationType.AddRange, addRangeElapsedTime);

            return recipes;
        }

        private static void AddRecipeMedicaments(IStatelessSession session, List<Recipe> recipes, List<Medicament> medicaments)
        {
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

            var addRangeElapsedTime = StopwatchHelper.MeasureExecutionTime(() =>
            {
                foreach (var nurse in list)
                {
                    session.Insert(nurse);
                }
            });
            Logger.WriteCsvLog(OrmType.NHibernate, InheritanceType.TPT, TableType.RecipeMedicaments, OperationType.AddRange, addRangeElapsedTime);

            //List<Recipe> recipes = session.Recipes.Where(x => x.Id != null).ToList();
            //foreach (var recipe in recipes)
            //{
            //    Medicament medicament = session.Medicaments.FirstOrDefault();
            //    recipe.RecipeMedicaments.Add(medicament);
            //    await session.AddMedicamentToRecipe(recipe);
            //}
        }
    }
}
