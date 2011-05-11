using StructureMap.Configuration.DSL;

namespace __NAME__.Configuration.Booststrapping
{
    public class __NAME__CoreRegistry : Registry
    {
        public __NAME__CoreRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.LookForRegistries();
                     });
        }
    }
}