namespace NHibernateProject.Model
{
    public class Department
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int PhoneNumber { get; set; }
        public virtual IList<Employee> Employees { get; set; }
    }
}
