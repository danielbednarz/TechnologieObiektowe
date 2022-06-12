using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class TechnicalWorkerMap : SubclassMap<TechnicalWorker>
    {
        public TechnicalWorkerMap()
        {
            DiscriminatorValue(@"TechnicalWorker");
            Map(x => x.Role);
        }
    }
}
