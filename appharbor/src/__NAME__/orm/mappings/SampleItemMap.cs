namespace __NAME__.orm.mappings
{
    using model;

    public class SampleItemMap : BaseMap<SampleItem>
    {
        public SampleItemMap() :base()
        {
            Table("SampleItems");
          
            Map(x => x.name);
            Map(x => x.firstname);
            Map(x => x.lastname);
        }
    }
}