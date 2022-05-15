using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.NetCore;
using NHibernate.Tool.hbm2ddl;
using NHibernateProject.Model;

namespace NHibernateProject.Infrastructure
{
    public static class NHibernateExtensions
    {
        public static ISession OpenSession(string connectionString)
        {
            // expose configuration to comment

            StoreConfiguration cfg = new StoreConfiguration();
            var sessionFactory = Fluently.Configure()
               .Database(
                  MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql())
                  .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Visit>(cfg)))
                  .ExposeConfiguration(y => new SchemaExport(y).Create(true, true))
                  .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            //var mapper = new ModelMapper();
            //mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            //HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            //var configuration = new Configuration();
            //configuration.DataBaseIntegration(c =>
            //{
            //    c.Dialect<MsSql2012Dialect>();
            //    c.ConnectionString = connectionString;
            //    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            //    c.SchemaAction = SchemaAutoAction.Validate;
            //    c.LogFormattedSql = true;
            //    c.LogSqlInConsole = true;
            //});
            //configuration.AddMapping(domainMapping);

            //// Tworzy i aktualizuje tabele w bazie danych
            //SchemaUpdate schemaUpdate = new SchemaUpdate(cfg);
            //schemaUpdate.Execute(false, true);


            //var sessionFactory = configuration.BuildSessionFactory();

            //OpenSession(connectionString);
            //services.AddSingleton(sessionFactory);
            //services.AddScoped(factory => sessionFactory.OpenSession());
            //services.AddScoped<MainDatabaseContext>();

            return services;
        }
    }

}