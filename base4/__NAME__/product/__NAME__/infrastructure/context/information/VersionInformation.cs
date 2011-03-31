namespace __NAME__.infrastructure.context.information
{
    using System.Diagnostics;
    using System.Reflection;

    public sealed class VersionInformation
    {
        public static string get_version()
        {
            string version = string.Empty;

            Assembly reference_assembly = Assembly.GetExecutingAssembly();
            string location = reference_assembly.Location;
            if (!string.IsNullOrEmpty(location))
            {
                version = FileVersionInfo.GetVersionInfo(location).FileVersion;
            }

            return version;
        }
    }
}