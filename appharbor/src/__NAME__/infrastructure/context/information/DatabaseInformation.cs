namespace __NAME__.infrastructure.context.information
{
    using configuration;
    using extensions;

    public sealed class DatabaseInformation
    {

        private static SystemConfiguration _config;

        private static SystemConfiguration initialize_config_to_dotnetconfig()
        {
            return new DotNetConfiguration();
        }

        public static string get_database_name()
        {
            if (_config == null)
            {
                _config = initialize_config_to_dotnetconfig();
            }

            string database_name = string.Empty;

            string connection_string = _config.get_connection_string("db");
            if (!string.IsNullOrEmpty(connection_string))
            {
                string[] parts = connection_string.Split(';');
                foreach (string part in parts)
                {
                    if ((part.to_lower().Contains("initial catalog") || part.to_lower().Contains("database")))
                    {
                        database_name = part.Substring(part.IndexOf("=") + 1);
                        break;
                    }
                }
            }

            return database_name;
        }

    }
}