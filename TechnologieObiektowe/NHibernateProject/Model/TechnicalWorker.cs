namespace NHibernateProject.Model
{
    public class TechnicalWorker : Employee
    {
        public virtual int Id { get; set; }
        public virtual string Role { get; set; }    
    }
}
