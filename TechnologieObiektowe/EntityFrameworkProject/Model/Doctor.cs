namespace EntityFrameworkProject
{
    public class Doctor : Employee
    {
        public string Specialization { get; set; }

        public ICollection<Visit> Visits { get; set; }
    }
}
