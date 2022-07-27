using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace NHibernateSample.Domain
{
    public class FluentSessionFactory
    {
        private static readonly string DB_FILE_NAME = "hibernateSample.db";
        private static ISessionFactory sessionFactory;

        public static ISessionFactory GetInstance()
        {
            if(sessionFactory == null)
            {
                sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(DB_FILE_NAME))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Person>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            return sessionFactory;
        }

        private static void BuildSchema(Configuration config)
        {
            if (!File.Exists(DB_FILE_NAME))
            {
                new SchemaExport(config).Create(false, true);
            }
        }
    }
}
