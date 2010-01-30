namespace __NAME__.infrastructure.app.settings
{
    using System.Configuration;

    /// <summary>
    /// Configuration Handler for Bombali
    /// </summary>
    public sealed class __NAME__Configuration : ConfigurationSection
    {
        static readonly __NAME__Configuration _settings =
            ConfigurationManager.GetSection("__NAME__") as __NAME__Configuration;

        /// <summary>
        /// Settings section
        /// </summary>
        public static __NAME__Configuration settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// Who will the email be coming from
        /// </summary>
        [ConfigurationProperty("emailFrom", IsRequired = true)]
        public string email_from
        {
            get { return (string)this["emailFrom"]; }
        }

        /// <summary>
        /// The smtp Host server
        /// </summary>
        [ConfigurationProperty("smtpHost", IsRequired = true)]
        public string smtp_host
        {
            get { return (string)this["smtpHost"]; }
        }

    }
}