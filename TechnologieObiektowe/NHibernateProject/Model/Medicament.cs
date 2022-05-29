namespace NHibernateProject.Model
{
    public class Medicament
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Company { get; set; }
        public virtual IList<RecipeMedicament> RecipeMedicaments { get; set; }
        //public virtual IList<Recipe> RecipeMedicaments { get; set; }
    }
}
