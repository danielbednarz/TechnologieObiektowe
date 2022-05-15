namespace DataGenerator
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        public ICollection<EmployeeVM> Employees { get; set; }
    }
}
