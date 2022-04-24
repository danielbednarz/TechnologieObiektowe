namespace NHibernateProject.Model
{
    public class RecipeMedicament
    {
        public virtual int Id { get; set; }   
        public virtual int RecipeId { get; set; }   
        public virtual int MedicamentId { get; set; }   
    }
}
