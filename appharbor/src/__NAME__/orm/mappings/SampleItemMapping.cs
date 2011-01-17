namespace __NAME__.orm.mappings
{
    using FluentNHibernate.Mapping;
    using model;

    public class SampleItemMapping : ClassMap<SampleItem>
    {
        public SampleItemMapping() :base()
        {
            HibernateMapping.Schema("dbo");
            Table("SampleItems");
            Not.LazyLoad();
            HibernateMapping.DefaultAccess.Property();
            HibernateMapping.DefaultCascade.SaveUpdate();

            Id(x => x.id).Column("Id").GeneratedBy.Identity().UnsavedValue(0);
            Map(x => x.name);
            Map(x => x.firstname);
            Map(x => x.lastname);
        }
    }
}