namespace __NAME__.deployment
{
    using System.IO;
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
        public static Role Website { get; set; }
        public static Role VirtualDirectory { get; set; }

        public TheDeployment()
        {
            Define(settings =>
                   {

                        DeploymentStepsFor(Website, s =>
                       {
                           s.CopyDirectory("..\\_PublishedWebSites\\__NAME__.web")
                               .To(@"{{WebsitePath}}")
                               .DeleteDestinationBeforeDeploying();

                           s.CopyFile(@"..\EnvironmentFiles\{{Environment}}\{{Environment}}.web.config")
                               .ToDirectory(@"{{WebsitePath}}")
                               .RenameTo(@"web.config");

                           //s.Security(securityOptions =>
                           //{
                           //    securityOptions.ForPath(settings.WebsitePath, fileSecurityConfig => fileSecurityConfig.GrantRead(settings.AppUsername));
                           //    securityOptions.ForPath(settings.LogDirectory, fs => fs.GrantReadWrite(settings.AppUsername));
                           //    securityOptions.ForPath(@"~\C$\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files", fs => fs.GrantReadWrite(settings.AppUsername));
                           //    if (Directory.Exists(@"~\C$\Windows\Microsoft.NET\Framework64\v2.0.50727\Temporary ASP.NET Files"))
                           //    {
                           //        securityOptions.ForPath(@"~\C$\Windows\Microsoft.NET\Framework64\v2.0.50727\Temporary ASP.NET Files", fs => fs.GrantReadWrite(settings.AppUsername));
                           //    }
                           //});
                       });

                       DeploymentStepsFor(VirtualDirectory, s =>
                       {
                           s.Iis7Site("Default")
                            .VirtualDirectory(settings.VirtualDirectoryName)
                            .SetAppPoolTo("__NAME__.{{Environment}}", a => { 
                                                        a.Enable32BitAppOnWin64();
                                                        a.UseClassicPipeline();
                                                        })
                            .SetPathTo(@"{{WebsitePath}}");
                       });
                   });
        }

    }
}