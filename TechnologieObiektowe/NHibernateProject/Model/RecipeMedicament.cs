namespace NHibernateProject.Model
{
    public class RecipeMedicament
    {
        public virtual int Id { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Medicament Medicament { get; set; }
    }
}
