namespace DataGenerator
{
    public static class MedicamentsGenerator
    {
        private static string GenerateMedicamentName()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, random.Next(4, 15)).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GenerateMedicamentType()
        {
            List<string> types = new List<string>() { "tabletki", "kapsulki", "syrop", "zastrzyk", "proszek", "zel", "krem" };
            Random random = new Random();
            return types[random.Next(0, types.Count - 1)];
        }

        private static string GenerateMedicamentCompany()
        {
            List<string> types = new List<string>() { "Pfizer", "Johnson&Johnson", "AstraZeneca", "GSK", "Novartis", "Bayer", "Roche", "Merck", "Gilead" };
            Random random = new Random();
            return types[random.Next(0, types.Count - 1)];
        }

        public static List<MedicamentVM> GenerateMedicaments(int n)
        {
            List<MedicamentVM> medicaments = new List<MedicamentVM>();

            for (int i = 0 ; i < n; i++)
            {
                MedicamentVM medicament = new MedicamentVM()
                {
                    Name = GenerateMedicamentName(),
                    Type = GenerateMedicamentType(),
                    Company = GenerateMedicamentCompany()
                };

                medicaments.Add(medicament);
            }

            return medicaments;
        }
    }
}