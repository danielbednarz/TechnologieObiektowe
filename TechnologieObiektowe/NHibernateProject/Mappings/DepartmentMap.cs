using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.PhoneNumber);
            HasMany(x => x.Employees)
              .Inverse()
              .KeyColumn("DepartmentId")
              .Cascade.All();

            Table("Departments");
        }
    }
}
