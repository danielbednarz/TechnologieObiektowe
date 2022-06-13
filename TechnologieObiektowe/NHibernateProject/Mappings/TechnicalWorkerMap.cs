﻿using FluentNHibernate.Mapping;
using NHibernateProject.Model;

namespace NHibernateProject.Mappings
{
    public class TechnicalWorkerMap : SubclassMap<TechnicalWorker>
    {
        public TechnicalWorkerMap()
        {
            Abstract();
            Map(x => x.Role);

            Table("TechnicalWorkers");
        }
    }
}
