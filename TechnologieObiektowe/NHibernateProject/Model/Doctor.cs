namespace NHibernateProject.Model
{
    public class Doctor : Employee
    {
        public virtual int Id { get; set; }
        public virtual string Specialization { get; set; }    
    }
}
