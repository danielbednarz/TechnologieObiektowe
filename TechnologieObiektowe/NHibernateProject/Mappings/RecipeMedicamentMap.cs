using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class RecipeMedicamentMap : ClassMapping<RecipeMedicament>
    {
        public RecipeMedicamentMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
            });

            Table("RecipeMedicaments");
        }
    }
}
