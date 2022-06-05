namespace DataGenerator
{
    public static class PatientsGenerator
    {
        static readonly string[] MaleNames = { "Adam", "Andrzej", "Bogumił", "Czesław", "Damian", "Dariusz", "Emil", "Franciszek", "Grzegorz", "Hugo", "Igor", "Jan", "Paweł" };
        static readonly string[] MaleSurnames = { "Nowak", "Kowalski", "Wiśniewski", "Wojcik", "Kowalczyk", "Kamiński", "Lewandowski", "Zieliński", "Woźniak", "Szymański" };
        static readonly string[] FemaleNames = { "Weronika", "Karolina", "Dominika", "Dorota", "Alicja", "Genowefa", "Stanisława", "Marianna", "Bożena", "Grażyna", "Sandra", "Iza" };
        static readonly string[] FemaleSurnames = { "Kowalska", "Wiśniewska", "Dąbrowska", "Kozłowska", "Mazur", "Kwiatkowska", "Krawczyk", "Piotrowska", "Grabowska", "Kaczmarek" };

        public static List<PatientVM> GeneratePatients(int n)
        {
            Random random = new();
            List<PatientVM> patients = new();
            List<DepartmentVM> departments = DepartmentsGenerator.GenerateDepartments();

            for (int i = 0; i < n; i++)
            {
                bool isFemale = false;
                if (random.Next(0, 2) == 1)
                {
                    isFemale = true;
                }
                PatientVM patient = new()
                {
                    Name = isFemale ? FemaleNames[random.Next(FemaleNames.Length)] : MaleNames[random.Next(MaleNames.Length)],
                    Surname = isFemale ? FemaleSurnames[random.Next(FemaleSurnames.Length)] : MaleSurnames[random.Next(MaleSurnames.Length)],
                    BirthDate = GeneratorHelper.GenerateBirthDate(),
                    Address = GeneratorHelper.GenerateRandomString(),
                    Gender = isFemale ? 1 : 0,
                };

                patients.Add(patient);
            }

            return patients;
        }

    }
}
