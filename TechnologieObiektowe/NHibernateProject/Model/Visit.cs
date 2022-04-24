namespace NHibernateProject.Model
{
    public class Visit
    {
        public virtual int Id { get; set; }
        public virtual DateTime VisitDate { get; set; }
        public virtual string Diagnosis { get; set; }
        public virtual string Description { get; set; }
        public virtual double Cost { get; set; }
    }
}
