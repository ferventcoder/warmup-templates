namespace __NAME__.infrastructure.configuration
{
    using System.Configuration;

    public class DotNetConfiguration : SystemConfiguration
    {
        public string get_value_string(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string get_connection_string(string name)
        {
            string connection_string = string.Empty;
            ConnectionStringSettings connection_string_settings = ConfigurationManager.ConnectionStrings[name];
            if (connection_string_settings != null)
            {
                connection_string = connection_string_settings.ConnectionString;    
            }

            return connection_string;
        }

    }
}