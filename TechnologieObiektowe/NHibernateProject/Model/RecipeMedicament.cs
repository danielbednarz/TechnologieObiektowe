namespace NHibernateProject.Model
{
    public class RecipeMedicament
    {
        public virtual int? RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual int? MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as RecipeMedicament;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.RecipeId == other.RecipeId &&
                this.MedicamentId == other.MedicamentId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ RecipeId.GetHashCode();
                hash = (hash * 31) ^ MedicamentId.GetHashCode();

                return hash;
            }
        }

    }
}
