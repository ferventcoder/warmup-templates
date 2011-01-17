namespace FHLBank.Cue.Deployment
{
    using dropkick.Configuration;
    using dropkick.Configuration.Dsl.Security;

    public class DeploymentSettings : 
        DropkickConfiguration
    {
        //directories
        public string LogDirectory { get; set; }
        public string WebsitePath { get; set; }
        public string FileServerPath { get; set; }
        public string HostServicePath { get; set; }

        //service info
        public string AppUsername { get; set; }
        public string AppPassword { get; set; }

        //web stuff
        public string VirtualDirectoryName {get; set;}

        //message queues information
        public string MSMQName { get; set; }
        public string MTPubSubMSMQName { get; set; }
        public string MTPubSubUserName { get; set; }
        
        //security
        public Rights ProdReadAcl { get; set; }
    }
}
