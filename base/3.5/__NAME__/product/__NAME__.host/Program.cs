namespace __NAME__.host
{
    using System;
    using System.ServiceProcess;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if((args.Length > 0) && (Array.IndexOf(args, "/console") != -1))
            {
                Service service = new Service();
                service.RunConsole(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                                    {
                                        new Service()
                                    };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}