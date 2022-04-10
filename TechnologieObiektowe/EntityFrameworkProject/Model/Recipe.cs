namespace EntityFrameworkProject
{
    public class Recipe
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public ICollection<Medicament> Medicaments { get; set; }
        //public IEnumerable<int> VisitId { get; set; }
    }
}
