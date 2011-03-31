namespace __NAME__.infrastructure.configuration
{
    public interface SystemConfiguration
    {
        string get_value_string(string key);
        string get_connection_string(string name);
    }
}