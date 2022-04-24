using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class VisitMap : ClassMapping<Visit>
    {
        public VisitMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
            });

            Property(b => b.VisitDate, x =>
            {
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
            });

            Property(b => b.Diagnosis, x =>
            {
                x.Type(NHibernateUtil.StringClob);
            });

            Property(b => b.Description, x =>
            {
                x.Type(NHibernateUtil.StringClob);
            });

            Property(b => b.Cost, x =>
            {
                x.Type(NHibernateUtil.Double);
            });

            Property(b => b.VisitDate, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
            });
            

            Table("Visits");
        }
    }
}
