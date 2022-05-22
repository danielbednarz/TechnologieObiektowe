namespace EntityFrameworkProject
{
    public class MedicamentRecipe
    {
        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
