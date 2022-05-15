using DataGenerator;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProject
{
    public class DbInitializer
    {
        const int visitCount = 10000;
        const int doctorCount = 40;
        const int patientCount = 1000;

        public static void Seed(MainDatabaseContext context)
        {
            context.Database.Migrate();

            AddMedicaments(context);
            AddDepartments(context);
            AddNurses(context);
            AddTechnicalWorkers(context);
            int firstDoctorId = AddDoctors(context);
            AddPatients(context);
            AddVisits(context, firstDoctorId);
            AddRecipes(context);
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

            context.Medicaments.AddRange(medicaments);
            context.SaveChanges();
        }

        private static void AddDepartments(MainDatabaseContext context)
        {
            if(context.Departments.Any())
            {
                return;
            }

            var departmentsVM = DepartmentsGenerator.GenerateDepartments();

            List<Department> departments = departmentsVM.Select(x => new Department()
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();

            context.Departments.AddRange(departments);
            context.SaveChanges();
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

            context.AddRange(nurses);
            context.SaveChanges();
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

            context.TechnicalWorkers.AddRange(technicalWorkers);
            context.SaveChanges();
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

            context.Doctors.AddRange(doctors);
            context.SaveChanges();

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

            context.Patients.AddRange(patients);
            context.SaveChanges();
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

            context.Visits.AddRange(visits);
            context.SaveChanges();
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

            context.Recipes.AddRange(recipes);
            context.SaveChanges();
        }
    }
}
