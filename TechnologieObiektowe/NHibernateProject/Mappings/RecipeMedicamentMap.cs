using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class RecipeMedicamentMap : ClassMap<RecipeMedicament>
    {
        public RecipeMedicamentMap()
        {
            Id(x => x.Id);
            References(x => x.Recipe).Column("RecipeId");
            References(x => x.Medicament).Column("MedicamentId");

            Table("RecipeMedicaments");
        }
    }
}
