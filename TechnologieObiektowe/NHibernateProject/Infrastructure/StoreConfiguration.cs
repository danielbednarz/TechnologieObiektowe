using FluentNHibernate.Automapping;

namespace NHibernateProject.Infrastructure
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "NHibernateProject.Model";
        }
    }
}
