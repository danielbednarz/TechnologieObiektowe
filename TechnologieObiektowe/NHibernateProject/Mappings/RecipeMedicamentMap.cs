using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class RecipeMedicamentMap : ClassMap<RecipeMedicament>
    {
        public RecipeMedicamentMap()
        {
            CompositeId().KeyProperty(x => x.RecipeId, "RecipeId").KeyProperty(x => x.MedicamentId, "MedicamentId");
            References(x => x.Recipe).Column("RecipeId").Not.Insert().Not.Update();
            References(x => x.Medicament).Column("MedicamentId").Not.Insert().Not.Update();

            Table("RecipeMedicaments");
        }
    }
}
