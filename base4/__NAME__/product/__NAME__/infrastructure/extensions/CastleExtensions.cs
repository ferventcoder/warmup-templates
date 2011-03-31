namespace __NAME__.infrastructure.extensions
{
    using System.Configuration;
    using Castle.MicroKernel.Registration;

    public static class CastleExtensions
    {
        public static Property from_app_settings(this PropertyKey pk, string key)
        {
            return pk.Eq(ConfigurationManager.AppSettings[key]);
        }
    }
}