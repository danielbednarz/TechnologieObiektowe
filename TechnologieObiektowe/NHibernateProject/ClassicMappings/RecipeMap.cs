//using NHibernate;
//using NHibernate.Mapping.ByCode;
//using NHibernate.Mapping.ByCode.Conformist;
//using NHibernateProject.Model;

//namespace NHibernateProject.Mappings
//{
//    public class RecipeMap : ClassMapping<Recipe>
//    {
//        public RecipeMap()
//        {
//            Id(x => x.Id, x =>
//            {
//                x.Generator(Generators.Increment);
//                x.Type(NHibernateUtil.Int32);
//                x.Column("Id");
//                x.UnsavedValue(0);
//            });

//            Property(b => b.IssueDate, x =>
//            {
//                x.Type(NHibernateUtil.DateTime);
//                x.NotNullable(true);
//            });

//            Bag(x => x.Medicaments, collectionMapping =>
//            {
//                collectionMapping.Table("RecipeMedicaments");
//                collectionMapping.Cascade(Cascade.None);
//                collectionMapping.Key(k => k.Column("RecipeId"));
//            },
//                map => map.ManyToMany(p => p.Column("MedicamentId"))
//            );


//            Table("Recipes");
//        }
//    }
//}
