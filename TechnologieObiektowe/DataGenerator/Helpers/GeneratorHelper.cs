namespace DataGenerator
{
    public static class GeneratorHelper
    {
        public static string GenerateRandomString()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, random.Next(4, 15)).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomLongString()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, random.Next(50, 200)).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GeneratePhoneNumber()
        {
            Random random = new();
            string phoneNumber = string.Empty;

            for (int i = 0; i < 9; i++)
            {
                phoneNumber = string.Concat(phoneNumber, random.Next(10).ToString());
            }

            return int.Parse(phoneNumber);
        }

        public static DateTime GenerateBirthDate()
        {
            Random gen = new();
            DateTime start = new DateTime(1950, 1, 1);
            DateTime end = new DateTime(2004, 12, 31);
            int range = (end - start).Days;

            return start.AddDays(gen.Next(range));
        }

        public static DateTime GenerateVisitDate()
        {
            Random gen = new();
            DateTime start = new DateTime(2010, 1, 1);
            DateTime end = DateTime.Today;
            int range = (end - start).Days;

            return start.AddDays(gen.Next(range));
        }
    }
}
