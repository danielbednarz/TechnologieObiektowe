using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            UseUnionSubclassForInheritanceMapping();
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.BirthDate);
            Map(x => x.Gender);
            Map(x => x.Address);
            Map(x => x.Salary);
            Map(x => x.DepartmentId);
            References(x => x.Department).Column("DepartmentId").Not.Insert().Not.Update();

            Table("Employees");
        }
    }
}
