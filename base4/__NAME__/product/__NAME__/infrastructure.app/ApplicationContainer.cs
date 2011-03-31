namespace __NAME__.infrastructure.app
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Configuration;
    using filesystem;
    using NHibernate;
    using persistence;
    using persistence.custom;

    public class ApplicationContainer : WindsorContainer
    {
        public ApplicationContainer()
            : base()
        {
            initialize_ioc();
        }

        public ApplicationContainer(IConfigurationInterpreter interpreter)
            : base(interpreter)
        {
            initialize_ioc();
        }

        public void initialize_ioc()
        {
            AddComponent<FileSystemAccess, DotNetFileSystemAccess>();

            Kernel.AddComponentInstance("db_sessionfactory", typeof(ISessionFactory), NHibernateSessionFactory.build_session_factory("db"));

            Register(Component.For<IRepository>().ImplementedBy<NHibernateRepository>().Named("db_repository")
                    .Parameters(
                        Parameter.ForKey("session_factory").Eq("${db_sessionfactory}")
                    )
                );
        }

    }
}
