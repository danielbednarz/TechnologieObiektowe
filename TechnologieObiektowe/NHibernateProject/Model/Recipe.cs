namespace NHibernateProject.Model
{
    public class Recipe
    {
        public virtual int Id { get; set; }
        public virtual DateTime IssueDate { get; set; }
        public virtual int? VisitId { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual IList<RecipeMedicament> RecipeMedicaments { get; set; }
        //public virtual IList<Medicament> RecipeMedicaments { get; set; }
    }
}
