namespace NHibernateProject.Model
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual int Gender { get; set; }
        public virtual string Address { get; set; }
        public virtual double Salary { get; set; }
        public virtual Department Department { get; set; }
    }
}
