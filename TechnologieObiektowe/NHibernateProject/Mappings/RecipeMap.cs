using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class RecipeMap : ClassMap<Recipe>
    {
        public RecipeMap()
        {
            Id(x => x.Id);
            Map(x => x.IssueDate);
            //HasManyToMany(x => x.RecipeMedicaments)
            // .Cascade.All()
            // .ParentKeyColumn("RecipeId")
            // .ChildKeyColumn("MedicamentId")
            // .Table("RecipeMedicaments");
            HasMany(x => x.RecipeMedicaments)
              .Inverse()
              .Cascade.All()
              .Table("RecipeMedicaments");
            Map(x => x.VisitId);
            References(x => x.Visit).Column("VisitId").Not.Insert().Not.Update();

            Table("Recipes");
        }
    }
}
