using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class MedicamentMap : ClassMapping<Medicament>
    {
        public MedicamentMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
            });

            Property(b => b.Name, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });

            Property(b => b.Type, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(false);
            });

            Property(b => b.Company, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(false);
            });

            Bag(x => x.Recipes, collectionMapping =>
            {
                collectionMapping.Table("RecipeMedicaments");
                collectionMapping.Cascade(Cascade.None);
                collectionMapping.Key(k => k.Column("MedicamentId"));
            },
                map => map.ManyToMany(p => p.Column("RecipeId"))
            );

            

            Table("Medicaments");
        }
    }
}
