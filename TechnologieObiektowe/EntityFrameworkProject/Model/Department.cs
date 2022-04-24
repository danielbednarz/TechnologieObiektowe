namespace EntityFrameworkProject
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
