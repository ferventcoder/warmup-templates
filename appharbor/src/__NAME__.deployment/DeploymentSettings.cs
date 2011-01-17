namespace __NAME__.deployment
{
    using dropkick.Configuration;
    using dropkick.Configuration.Dsl.Security;

    public class DeploymentSettings : 
        DropkickConfiguration
    {
        //directories
        public string LogDirectory { get; set; }
        public string WebsitePath { get; set; }
       
        //web stuff
        public string VirtualDirectoryName {get; set;}
        
    }
}
