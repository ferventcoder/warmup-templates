using FubuMVC.Core;

namespace __NAME__.Web.Configuration
{
    public class __NAME__Registry : FubuRegistry
    {
        public __NAME__Registry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();
        }
    }
}