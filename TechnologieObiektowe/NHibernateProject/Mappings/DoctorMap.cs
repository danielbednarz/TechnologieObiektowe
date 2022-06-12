using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class DoctorMap : SubclassMap<Doctor>
    {
        public DoctorMap()
        {
            DiscriminatorValue(@"Doctor");
            Map(x => x.Specialization);
            HasMany(x => x.Visits)
              .Inverse()
              .KeyColumn("DoctorId")
              .Cascade.All();
        }
    }
}
