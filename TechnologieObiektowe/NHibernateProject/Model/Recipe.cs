namespace NHibernateProject.Model
{
    public class Recipe
    {
        public virtual int Id { get; set; }
        public virtual DateTime IssueDate { get; set; }
        public virtual IList<Medicament> Medicaments { get; set; }
    }
}
