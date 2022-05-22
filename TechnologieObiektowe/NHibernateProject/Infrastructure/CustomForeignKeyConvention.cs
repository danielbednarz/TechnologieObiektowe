using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateProject.Infrastructure
{
    public class CustomForeignKeyConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Class.Name + "Id");
        }
    }

}
