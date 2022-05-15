namespace DataGenerator
{
    public class VisitVM 
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
