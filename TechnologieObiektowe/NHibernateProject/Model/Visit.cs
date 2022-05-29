namespace NHibernateProject.Model
{
    public class Visit
    {
        public virtual int Id { get; set; }
        public virtual DateTime VisitDate { get; set; }
        public virtual string Diagnosis { get; set; }
        public virtual string Description { get; set; }
        public virtual double Cost { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual IList<Recipe> Recipes { get; set; }

    }
}
