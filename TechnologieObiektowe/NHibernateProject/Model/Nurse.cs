namespace NHibernateProject.Model
{
    public class Nurse : Employee
    {
        public virtual int Id { get; set; }
        public virtual string Role { get; set; }    
    }
}
