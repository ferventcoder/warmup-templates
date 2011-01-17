using System.IO;

namespace FHLBank.Cue.Deployment
{
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Iis;
    using dropkick.Configuration.Dsl.Files;
    using dropkick.Configuration.Dsl.WinService;
    using dropkick.Wmi;
    using dropkick.Configuration.Dsl.Msmq;
    using dropkick.Configuration.Dsl.Security;

    public class TheDeployment :
        Deployment<TheDeployment, DeploymentSettings>
    {
        //this order is important
        public static Role MessageQueue { get; set; }
        public static Role MTPubSubMSMQ { get; set; }
        public static Role File { get; set; }
        public static Role Website { get; set; }
        public static Role VirtualDirectory { get; set; }
        public static Role Host { get; set; }


        public TheDeployment()
        {
            Define(settings =>
                   {

                       DeploymentStepsFor(MessageQueue, s =>
                       {
                           string queuename = settings.MSMQName;

                           s.Msmq().PrivateQueueNamed(queuename);

                           s.Security(so =>
                           {
                               so.ForQueue(queuename, o =>
                               {
                                   o.SetSensibleMsmqDefaults();
                                   o.GrantReadWrite("{{AppUsername}}");
                                   o.GrantWrite("{{MTPubSubUserName}}");
                               });
                           });
                       });

                       DeploymentStepsFor(MTPubSubMSMQ, s =>
                       {
                           var queuename = settings.MTPubSubMSMQName;
                           s.Msmq().PrivateQueueNamed(queuename);

                           s.Security(so =>
                           {
                               so.ForQueue(queuename, o =>
                               {
                                   o.SetSensibleMsmqDefaults();
                                   o.GrantWrite("{{AppUsername}}");
                                   o.GrantReadWrite("{{MTPubSubUserName}}");
                               });
                           });
                       });

                       DeploymentStepsFor(File, s =>
                       {
                           string fileDirectory = @"{{FileServerPath}}";

                           s.CopyDirectory("..\\FileServer")
                               .To(fileDirectory);
                           //never delete the file server stuff. 
                           //Archived things like reports could be there. 
                           //.DeleteDestinationBeforeDeploying();

                           s.Security(securityOptions =>
                           {
                               securityOptions.ForPath(fileDirectory, fs => fs.GrantRead(settings.AppUsername));
                               securityOptions.ForPath(@"{{FileServerPath}}\TriggerWatch", fs => fs.GrantReadWrite(settings.AppUsername));
                               securityOptions.ForPath(fileDirectory, fs => fs.Grant(settings.ProdReadAcl, @"FHLB10\Prod Read"));
                           });

                           s.CopyFile(@"..\EnvironmentFiles\{{Environment}}\{{Environment}}.FHLBank.Cue.PostDeploy.dll.config")
                              .ToDirectory(@"{{FileServerPath}}\Test.E2E")
                              .RenameTo(@"FHLBank.Cue.PostDeploy.dll.config");
                       });

                       DeploymentStepsFor(Website, s =>
                       {
                           s.CopyDirectory("..\\_PublishedWebSites\\FHLBank.Cue.Web")
                               .To(@"{{WebsitePath}}")
                               .DeleteDestinationBeforeDeploying();

                           s.CopyFile(@"..\EnvironmentFiles\{{Environment}}\{{Environment}}.web.config")
                               .ToDirectory(@"{{WebsitePath}}")
                               .RenameTo(@"web.config");

                           s.Security(securityOptions =>
                           {
                               securityOptions.ForPath(settings.WebsitePath, fileSecurityConfig => fileSecurityConfig.GrantRead(settings.AppUsername));
                               securityOptions.ForPath(settings.LogDirectory, fs => fs.GrantReadWrite(settings.AppUsername));
                               securityOptions.ForPath(settings.WebsitePath, fileSecurityConfig => fileSecurityConfig.Grant(settings.ProdReadAcl, @"FHLB10\Prod Read"));
                               securityOptions.ForPath(@"~\C$\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files", fs => fs.GrantReadWrite(settings.AppUsername));
                               if (Directory.Exists(@"~\C$\Windows\Microsoft.NET\Framework64\v2.0.50727\Temporary ASP.NET Files"))
                               {
                                   securityOptions.ForPath(@"~\C$\Windows\Microsoft.NET\Framework64\v2.0.50727\Temporary ASP.NET Files", fs => fs.GrantReadWrite(settings.AppUsername));
                               }
                           });
                       });

                       DeploymentStepsFor(VirtualDirectory, s =>
                       {
                           s.Iis7Site("FHLB")
                            .VirtualDirectory(settings.VirtualDirectoryName)
                            .SetAppPoolTo("FHLBank-CUE{{Environment}}")
                            .SetPathTo(@"{{WebsitePath}}")
                            .UseClassicPipeline();
                       });

                       DeploymentStepsFor(Host, s =>
                       {
                           string serviceName = "FHLBank.Cue.{{Environment}}";
                           s.WinService(serviceName).Stop();

                           s.CopyDirectory("..\\FHLBank.Cue.Host")
                               .To(@"{{HostServicePath}}")
                               .DeleteDestinationBeforeDeploying();

                           s.CopyFile(@"..\EnvironmentFiles\{{Environment}}\{{Environment}}.FHLBank.Cue.Host.exe.config")
                               .ToDirectory(@"{{HostServicePath}}")
                               .RenameTo(@"FHLBank.Cue.Host.exe.config");

                           //need security to go before the start so that the app can have logon as service rights -dru
                           s.Security(o =>
                           {
                               o.LocalPolicy(lp =>
                               {
                                   lp.LogOnAsService(settings.AppUsername);
                                   lp.LogOnAsBatch(settings.AppUsername);
                               });

                               o.ForPath(settings.HostServicePath, fs => fs.GrantRead(settings.AppUsername));
                               o.ForPath(settings.LogDirectory, fs => fs.GrantReadWrite(settings.AppUsername));
                           });
                           s.WinService(serviceName).Delete();
                           s.WinService(serviceName).Create()
                               .WithCredentials(settings.AppUsername, settings.AppPassword)
                               .WithDisplayName("FHLBank CUE ({{Environment}})")
                               .WithServicePath(@"{{HostServicePath}}\FHLBank.Cue.Host.exe")
                               .WithStartMode((settings.Environment.Equals("DR") ? ServiceStartMode.Manual : ServiceStartMode.Automatic))
                               .AddDependency("MSMQ");

                           if (!settings.Environment.Equals("DR"))
                           {
                               s.WinService(serviceName).Start();
                           }
                       });

                   });
        }

    }
}