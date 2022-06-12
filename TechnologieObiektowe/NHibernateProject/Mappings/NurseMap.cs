using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class NurseMap : SubclassMap<Nurse>
    {
        public NurseMap()
        {
            DiscriminatorValue(@"Nurse");
            Map(x => x.Role);
        }
    }
}
