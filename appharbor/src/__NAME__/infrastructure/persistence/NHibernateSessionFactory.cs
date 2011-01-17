namespace __NAME__.infrastructure.persistence
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using listeners;
    using NHibernate;
    using NHibernate.Event;
    using orm.mappings;

    public class NHibernateSessionFactory
    {
        public static ISessionFactory build_session_factory(string database_config_name)
        {
            return Fluently.Configure()
                     .Database(MsSqlConfiguration.MsSql2005
                         .ConnectionString(c =>
                             c.FromConnectionStringWithKey(database_config_name)))
                     .Mappings(m =>
                     {
                         m.FluentMappings.AddFromAssemblyOf<SampleItemMap>();
                         m.HbmMappings.AddFromAssemblyOf<SampleItemMap>();
                     })
                     .ExposeConfiguration(cfg =>
                     {
                         cfg.SetListener(ListenerType.PreInsert, new AuditEventListener());
                         cfg.SetListener(ListenerType.PreUpdate, new AuditEventListener());
                     })
                     .BuildSessionFactory();
        }
    }
}