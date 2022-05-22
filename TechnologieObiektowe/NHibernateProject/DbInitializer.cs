using DataGenerator;
using NHibernateProject.Model;

namespace NHibernateProject
{
    public class DbInitializer
    {
        const int visitCount = 10000;
        const int doctorCount = 40;
        const int patientCount = 1000;

        public static async Task Seed(MainDatabaseContext context)
        {
            List<Medicament> medicaments = await AddMedicaments(context);
            List<Department> departments = await AddDepartments(context);
            await AddNurses(context, departments);
            await AddTechnicalWorkers(context, departments);
            List<Doctor> doctors = await AddDoctors(context, departments);
            List<Patient> patients = await AddPatients(context);
            List<Visit> visits = await AddVisits(context, doctors, patients);
            List<Recipe> recipes = await AddRecipes(context, visits);
            //await AddRecipeMedicaments(context, medicaments, recipes);
            await context.Commit();
        }

        private static async Task<List<Medicament>> AddMedicaments(MainDatabaseContext context)
        {
            List<MedicamentVM> medicamentsVM = MedicamentsGenerator.GenerateMedicaments(30);

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

            await context.AddRange(medicaments);

            return medicaments;
        }

        private static async Task<List<Department>> AddDepartments(MainDatabaseContext context)
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

            await context.AddRange(departments);

            return departments;
        }

        private static async Task AddNurses(MainDatabaseContext context, List<Department> departments)
        {
            if (context.Nurses.Any())
            {
                return;
            }

            var nursesVM = NursesGenerator.GenerateNurses(30);

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

            await context.AddRange(nurses);
        }

        private static async Task AddTechnicalWorkers(MainDatabaseContext context, List<Department> departments)
        {
            if (context.TechnicalWorkers.Any())
            {
                return;
            }

            var techicalWorkersVM = TechnicalWorkersGenerator.GenerateTechnicalWorkers(30);

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

            await context.AddRange(techicalWorkers);
        }

        private static async Task<List<Doctor>> AddDoctors(MainDatabaseContext context, List<Department> departments)
        {
            if (context.Doctors.Any())
            {
                return null;
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
                Salary = decimal.ToDouble(x.Salary),
                Department = departments.FirstOrDefault(y => y.Id == x.DepartmentId)
            }).ToList();

            await context.AddRange(doctors);

            // Pobierane jest ID pierwszego doktora
            return doctors;
        }

        private static async Task<List<Patient>> AddPatients(MainDatabaseContext context)
        {
            if (context.Patients.Any())
            {
                return null;
            }

            var patientsVM = PatientsGenerator.GeneratePatients(patientCount);

            List<Patient> patients = patientsVM.Select(x => new Patient()
            {
                Name = x.Name,
                Surname = x.Surname,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Address = x.Address
            }).ToList();

            await context.AddRange(patients);

            return patients;
        }

        private static async Task<List<Visit>> AddVisits(MainDatabaseContext context, List<Doctor> doctors, List<Patient> patients)
        {
            if (context.Visits.Any())
            {
                return null;
            }

            List<VisitVM> visitsVM = VisitsGenerator.GenerateVisits(visitCount, doctorCount, patientCount, doctors.FirstOrDefault().Id);

            List<Visit> visits = visitsVM.Select(x => new Visit
            {
                VisitDate = x.VisitDate,
                Diagnosis = x.Diagnosis,
                Description = x.Description,
                Cost = decimal.ToDouble(x.Cost),
                Patient = patients.FirstOrDefault(y => y.Id == x.PatientId),
                Doctor = doctors.FirstOrDefault(y => y.Id == x.DoctorId)
            }).ToList();

            await context.AddRange(visits);

            return visits;
        }

        private static async Task<List<Recipe>> AddRecipes(MainDatabaseContext context, List<Visit> visits)
        {
            if (context.Recipes.Any())
            {
                return null;
            }

            var recipesVM = RecipesGenerator.GenerateRecipes(8000, visitCount);

            List<Recipe> recipes = recipesVM.Select(x => new Recipe
            {
                IssueDate = x.IssueDate,
                Visit = visits.FirstOrDefault(y => y.Id == x.VisitId)
            }).ToList();

            await context.AddRange(recipes);

            return recipes;
        }

        //private static async Task AddRecipeMedicaments(MainDatabaseContext context, List<Medicament> medicaments, List<Recipe> recipes)
        //{
        //    Random random = new();

        //    List<RecipeMedicament> recipeMedicaments = recipes.Select(x => new RecipeMedicament()
        //    {
        //        MedicamentId = medicaments[random.Next(1, medicaments.Count)].Id,
        //        RecipeId = x.Id
        //    }).ToList();

        //    await context.AddRange(recipeMedicaments);
        //}
    }
}
