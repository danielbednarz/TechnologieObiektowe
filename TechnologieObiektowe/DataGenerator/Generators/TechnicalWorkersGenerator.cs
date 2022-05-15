namespace DataGenerator
{
    public static class TechnicalWorkersGenerator
    {
        static readonly string[] MaleNames = { "Adam", "Andrzej", "Bogumił", "Czesław", "Damian", "Dariusz", "Emil", "Franciszek", "Grzegorz", "Hugo", "Igor", "Jan", "Paweł" };
        static readonly string[] MaleSurnames = { "Nowak", "Kowalski", "Wiśniewski", "Wojcik", "Kowalczyk", "Kamiński", "Lewandowski", "Zieliński", "Woźniak", "Szymański" };
        static readonly string[] FemaleNames = { "Weronika", "Karolina", "Dominika", "Dorota", "Alicja", "Genowefa", "Stanisława", "Marianna", "Bożena", "Grażyna", "Sandra", "Iza" };
        static readonly string[] FemaleSurnames = { "Kowalska", "Wiśniewska", "Dąbrowska", "Kozłowska", "Mazur", "Kwiatkowska", "Krawczyk", "Piotrowska", "Grabowska", "Kaczmarek" };
        static readonly string[] Roles = { "Ciężkie prace", "Konserwator", "Palacz", "Sprzątanie", "Kucharz", "Monter" };

        public static List<NurseVM> GenerateTechnicalWorkers(int n)
        {
            Random random = new();
            List<NurseVM> technicalWorkers = new();

            List<DepartmentVM> departments = DepartmentsGenerator.GenerateDepartments();

            for (int i = 0; i < n; i++)
            {
                NurseVM technicalWorker = new()
                {
                    Name = MaleNames[random.Next(MaleNames.Length)],
                    Surname = MaleSurnames[random.Next(MaleSurnames.Length)],
                    Salary = random.Next(800, 10000),
                    BirthDate = GeneratorHelper.GenerateBirthDate(),
                    Role = Roles[random.Next(Roles.Length)],
                    Address = GeneratorHelper.GenerateRandomString(),
                    Gender = 0,
                    DepartmentId = departments[random.Next(departments.Count)].Id
                };

                technicalWorkers.Add(technicalWorker);
            }

            return technicalWorkers;
        }



    }
}
