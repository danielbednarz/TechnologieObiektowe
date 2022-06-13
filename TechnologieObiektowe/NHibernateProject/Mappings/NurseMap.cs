using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class NurseMap : SubclassMap<Nurse>
    {
        public NurseMap()
        {
            Abstract();
            Map(x => x.Role);

            Table("Nurses");
        }
    }
}
