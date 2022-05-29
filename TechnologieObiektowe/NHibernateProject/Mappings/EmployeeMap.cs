using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.BirthDate);
            Map(x => x.Gender);
            Map(x => x.Address);
            Map(x => x.Salary);
            References(x => x.Department).Column("DepartmentId");

            Table("Employees");
        }
    }
}
