namespace __NAME__.infrastructure.logging
{
    using System;
    using System.Collections.Generic;
    using containers;
    using custom;
    using log4net;

    public static class Log
    {
        private static bool have_displayed_error_message;

        private static IDictionary<object, Logger> log_dictionary;

        public static Logger bound_to(object object_that_needs_logging)
        {
            initialize_logger_dictionary_if_not_created();
            Logger logger;

            try
            {
                if (log_dictionary.ContainsKey(object_that_needs_logging))
                {
                    logger = log_dictionary[object_that_needs_logging];
                }
                else
                {

                    logger = Container.get_an_instance_of<LogFactory>().create_logger_bound_to(object_that_needs_logging);
                    add_to_dictionary(object_that_needs_logging, logger);
                }

            }
            catch (Exception)
            {
                
                logger = new Log4NetLogger(LogManager.GetLogger(object_that_needs_logging.ToString()));
                add_to_dictionary(object_that_needs_logging, logger);
                if (!have_displayed_error_message)
                {
                    logger.log_a_warning_event_containing("Logging wasn't set up in the container. Defaulting to log4net.");
                    have_displayed_error_message = true;
                }
            }

            return logger;
        }

        private static void add_to_dictionary(object object_that_needs_logging, Logger logger)
        {
            log_dictionary.Add(object_that_needs_logging, logger);
        }

        private static void initialize_logger_dictionary_if_not_created()
        {
            if (log_dictionary == null)
            {
                log_dictionary = new Dictionary<object, Logger>();
            }
        }
    }
}