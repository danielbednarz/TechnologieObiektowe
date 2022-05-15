namespace DataGenerator
{
    public static class NursesGenerator
    {
        static readonly string[] FemaleNames = { "Weronika", "Karolina", "Dominika", "Dorota", "Alicja", "Genowefa", "Stanisława", "Marianna", "Bożena", "Grażyna", "Sandra", "Iza" };
        static readonly string[] FemaleSurnames = { "Kowalska", "Wiśniewska", "Dąbrowska", "Kozłowska", "Mazur", "Kwiatkowska", "Krawczyk", "Piotrowska", "Grabowska", "Kaczmarek" };
        static readonly string[] Roles = { "Pielęgniarka", "Pomoc", "Opieka medyczna", "Nadzór pacjentów", "Zarządzanie" };

        public static List<NurseVM> GenerateNurses(int n)
        {
            Random random = new();
            List<NurseVM> nurses = new();

            List<DepartmentVM> departments = DepartmentsGenerator.GenerateDepartments();

            for (int i = 0; i < n; i++)
            {
                NurseVM nurse = new()
                {
                    Name = FemaleNames[random.Next(FemaleNames.Length)],
                    Surname = FemaleSurnames[random.Next(FemaleSurnames.Length)],
                    Salary = random.Next(800, 10000),
                    BirthDate = GeneratorHelper.GenerateBirthDate(),
                    Role = Roles[random.Next(Roles.Length)],
                    Address = GeneratorHelper.GenerateRandomString(),
                    Gender = 1,
                    DepartmentId = departments[random.Next(departments.Count)].Id
                };

                nurses.Add(nurse);
            }

            return nurses;
        }

    }
}
