namespace DataGenerator
{
    public static class DoctorsGenerator
    {
        static readonly string[] MaleNames = { "Adam", "Andrzej", "Bogumił", "Czesław", "Damian", "Dariusz", "Emil", "Franciszek", "Grzegorz", "Hugo", "Igor", "Jan", "Paweł" };
        static readonly string[] MaleSurnames = { "Nowak", "Kowalski", "Wiśniewski", "Wojcik", "Kowalczyk", "Kamiński", "Lewandowski", "Zieliński", "Woźniak", "Szymański" };
        static readonly string[] FemaleNames = { "Weronika", "Karolina", "Dominika", "Dorota", "Alicja", "Genowefa", "Stanisława", "Marianna", "Bożena", "Grażyna", "Sandra", "Iza" };
        static readonly string[] FemaleSurnames = { "Kowalska", "Wiśniewska", "Dąbrowska", "Kozłowska", "Mazur", "Kwiatkowska", "Krawczyk", "Piotrowska", "Grabowska", "Kaczmarek" };
        static readonly string[] Specializations = { "Alergolog", "Stomatolog", "Chirurg", "Kardiolog", "Ortopeda", "Ginekolog", "Pediatra" };

        public static List<DoctorVM> GenerateDoctors(int n)
        {
            Random random = new();
            List<DoctorVM> doctors = new();
            List<DepartmentVM> departments = DepartmentsGenerator.GenerateDepartments();
            bool isFemale = false;

            for (int i = 0; i < n; i++)
            {
                if (random.Next(0, 2) == 1)
                {
                    isFemale = true;
                }
                DoctorVM doctor = new()
                {
                    Name = isFemale ? FemaleNames[random.Next(FemaleNames.Length)] : MaleNames[random.Next(MaleNames.Length)],
                    Surname = isFemale ? FemaleSurnames[random.Next(FemaleSurnames.Length)] : MaleSurnames[random.Next(MaleSurnames.Length)],
                    Salary = random.Next(5000, 30000),
                    BirthDate = GeneratorHelper.GenerateBirthDate(),
                    Specialization = Specializations[random.Next(Specializations.Length)],
                    Address = GeneratorHelper.GenerateRandomString(),
                    Gender = isFemale ? 1 : 0,
                    DepartmentId = departments[random.Next(departments.Count)].Id
                };

                doctors.Add(doctor);
            }

            return doctors;
        }

    }
}
