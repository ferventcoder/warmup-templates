using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace __NAME__.host
{
    using System;
    using System.ServiceProcess;
    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using log4net;

    public partial class Service : ServiceBase
    {
        readonly ILog _logger = LogManager.GetLogger(typeof(Service));
        IWindsorContainer _container;

        protected override void OnStart(string[] args)
        {
            _logger.Info("Starting __NAME__ application...");
            try
            {
                InitializeIOC(args);
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("__NAME__ Service had an error on {0} (with user {1}):{3}{4}", Environment.MachineName, Environment.UserName,
                                    Environment.NewLine, ex.ToString());
                throw;
            }
        }

        protected void InitializeIOC(string[] args)
        {
            _logger.Debug("Attempting to initialize IOC container.");
            _container = new WindsorContainer(new XmlInterpreter());
        }

        protected override void OnStop()
        {
            try
            {
                _logger.InfoFormat("Shutting down __NAME__ service...");
                DisposeIOC();
            }
            catch(Exception ex)
            {
                _logger.Error("Error", ex);
                throw;
            }
        }

        public void DisposeIOC()
        {
            _logger.Debug("Disposing the IOC container.");
            _container.Dispose();
        }

        public void RunConsole(string[] args)
        {
            OnStart(args);
        }
    }
}