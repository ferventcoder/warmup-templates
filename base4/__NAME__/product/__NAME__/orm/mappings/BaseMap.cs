namespace __NAME__.orm.mappings
{
    using System;
    using FluentNHibernate.Mapping;
    using model;

    [CLSCompliant(false)]
    public abstract class BaseMap<ENTITY> : ClassMap<ENTITY> where ENTITY : class,IModel
    {
        protected BaseMap()
        {
            HibernateMapping.Schema("dbo");
            Not.LazyLoad();
            HibernateMapping.DefaultAccess.Property();
            HibernateMapping.DefaultCascade.SaveUpdate();

            Id(x => x.id).Column("Id").GeneratedBy.Identity().UnsavedValue(0);

        }

    }
}