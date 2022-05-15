namespace DataGenerator
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public virtual DepartmentVM Department { get; set; }
    }
}
