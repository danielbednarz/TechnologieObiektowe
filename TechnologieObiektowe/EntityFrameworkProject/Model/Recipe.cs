namespace EntityFrameworkProject
{
    public class Recipe
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public int VisitId { get; set; }
        public virtual Visit Visit { get; set; }
        public ICollection<RecipeMedicament> MedicamentRecipes { get; set; }
    }
}
