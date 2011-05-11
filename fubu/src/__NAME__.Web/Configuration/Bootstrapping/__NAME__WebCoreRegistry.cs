using __NAME__.Configuration.Booststrapping;
using StructureMap.Configuration.DSL;

namespace __NAME__.Web.Configuration.Bootstrapping
{
    public class __NAME__WebCoreRegistry : Registry
    {
        public __NAME__WebCoreRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.LookForRegistries();
                     });

            IncludeRegistry<__NAME__CoreRegistry>();
        }
    }
}