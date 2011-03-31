using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace __NAME__.console
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using infrastructure;
    using infrastructure.containers;
    using infrastructure.extensions;
    using infrastructure.filesystem;
    using log4net;
    using infrastructure.commandline.options;
    using log4net.Core;
    using log4net.Repository;

    internal class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            IList argument_list = new List<string>();
            foreach (string arg in args)
            {
                argument_list.Add(arg);
            }

            if (argument_list.Count == 1)
            {
                foreach (string argument in argument_list)
                {
                    if (argument.to_lower().Contains("version"))
                    {
                        report_version();
                    }
                }
            } 
            else
            {
                parse_arguments(args);
            }
        }
        public static void report_version()
        {
            string version = infrastructure.Version.get_current_assembly_version();
            _logger.InfoFormat("{0} - version {1}.",ApplicationParameters.name,version);
            
        }

        private static void parse_arguments(string[] args)
        {
            string yep = null;
            string files = null;

            bool help = false;

            OptionSet option_set = new OptionSet()
                .Add("?|help|h",
                    "Prints out the options.",
                    option => help = option != null)
                .Add("y=|yep=",
                    "REQUIRED: YEP - This is yep.",
                    option => yep = option)
                .Add("f=|files=|filesdirectory=",
                    "FilesDirectory - The directory where your something something.",
                    option => files = option)
               ;

            try
            {
                option_set.Parse(args);
            }
            catch (OptionException)
            {
                show_help("Error, usage is:", option_set);
            }

            if (help)
            {
                _logger.Info("Usage of __NAME__)");
                const string usage_message =
                    "__NAME__.console.exe /y[ep] VALUE [ /f[ilesdirectory] VALUE ]";
                show_help(usage_message, option_set);
            }

            if (yep == null)
            {
                show_help("Error: You must specify yep (/y).", option_set);
            }
        }

        public static void show_help(string message, OptionSet option_set)
        {
            //Console.Error.WriteLine(message);
            _logger.Info(message);
            option_set.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }

        public static void change_log_to_debug_level()
        {
            ILoggerRepository log_repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            log_repository.Threshold = Level.Debug;
        }

    }
}