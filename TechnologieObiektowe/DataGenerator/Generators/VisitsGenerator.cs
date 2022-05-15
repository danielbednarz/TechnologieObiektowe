namespace DataGenerator
{
    public static class VisitsGenerator
    {
        public static List<VisitVM> GenerateVisits(int n, int doctorCount, int patientCount, int firstDoctorId)
        {
            Random random = new();
            List<VisitVM> visits = new();

            for (int i = 0; i < n; i++)
            {
                VisitVM visit = new()
                {
                    VisitDate = GeneratorHelper.GenerateVisitDate(),
                    Diagnosis = GeneratorHelper.GenerateRandomString(),
                    Description = GeneratorHelper.GenerateRandomLongString(),
                    Cost = random.Next(50, 700),
                    PatientId = random.Next(1, patientCount),
                    DoctorId = random.Next(firstDoctorId, firstDoctorId + doctorCount)
                };

                visits.Add(visit);
            }

            return visits;
        }



    }
}
