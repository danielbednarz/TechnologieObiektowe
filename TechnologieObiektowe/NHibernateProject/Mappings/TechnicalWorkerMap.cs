using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class TechnicalWorkerMap : SubclassMap<TechnicalWorker>
    {
        public TechnicalWorkerMap()
        {
            KeyColumn("Id");
            Map(x => x.Role);

            Table("TechnicalWorkers");
        }
    }
}
