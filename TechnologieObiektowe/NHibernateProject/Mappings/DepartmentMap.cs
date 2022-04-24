using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class DepartmentMap : ClassMapping<Department>
    {
        public DepartmentMap()
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
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });

            Property(b => b.PhoneNumber, x =>
            {
                x.Type(NHibernateUtil.Int32);
            });

           

            Table("Departments");
        }
    }
}
