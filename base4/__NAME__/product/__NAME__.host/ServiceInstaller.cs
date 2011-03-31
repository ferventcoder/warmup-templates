namespace bombali.host
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        public ServiceInstaller()
        {
            InitializeComponent();

            ServiceProcessInstaller service_process_installer = new ServiceProcessInstaller();
            System.ServiceProcess.ServiceInstaller service_intaller = new System.ServiceProcess.ServiceInstaller();

            // ServiceProcessInstaller1
            service_process_installer.Password = null;
            service_process_installer.Username = null;

            //ServiceInstaller1
            service_intaller.Description =
                "Bombali monitors websites, databases, servers, etc and sends notifications when they go down. Kind of like Nagios.";
            service_intaller.DisplayName = "Bombali Monitoring Service";
            service_intaller.ServiceName = "Bombali";
            //service_intaller.ServicesDependedOn = new String[] {"MSMQ"};
            service_intaller.StartType = ServiceStartMode.Automatic;

            //ProjectInstaller
            Installers.AddRange(new Installer[] {service_process_installer, service_intaller});
        }
    }
}