namespace EntityFrameworkProject
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }

        public ICollection<Visit> Visits { get; set; }
    }
}
