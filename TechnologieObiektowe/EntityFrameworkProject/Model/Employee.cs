using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkProject
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
