using FluentNHibernate.Mapping;
using NHibernateProject.Model;
    
namespace NHibernateProject.Mappings
{
    public class MedicamentMap : ClassMap<Medicament>
    {
        public MedicamentMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Type);
            Map(x => x.Company);
            //HasManyToMany(x => x.RecipeMedicaments)
            // .Cascade.All()
            // .Inverse()
            // .ParentKeyColumn("MedicamentId")
            // .ChildKeyColumn("RecipeId")
            // .Table("RecipeMedicaments");
            HasMany(x => x.RecipeMedicaments)
              .Inverse()
              .KeyColumn("MedicamentId")
              .Cascade.All();

            Table("Medicaments");
        }
    }
}
