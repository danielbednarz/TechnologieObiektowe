using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class DoctorMap : SubclassMap<Doctor>
    {
        public DoctorMap()
        {
            KeyColumn("Id");
            Map(x => x.Specialization);
            HasMany(x => x.Visits)
              .Inverse()
              .KeyColumn("DoctorId")
              .Cascade.All();

            Table("Doctors");
        }
    }
}
