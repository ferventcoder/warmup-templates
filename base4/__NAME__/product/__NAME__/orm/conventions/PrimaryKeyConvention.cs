namespace __NAME__.orm.conventions
{
    using System;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    [CLSCompliant(false)]
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "ID");
        }
    }
}