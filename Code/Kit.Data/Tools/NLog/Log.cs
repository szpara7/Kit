using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kit
{
    public static class Log
    {
        private static ILogger _logger;

        static Log()
        {
            _logger = new LogFactory().LoadConfiguration("Tools/NLog/nlog.config").GetCurrentClassLogger();           
        }

        #region Error()
        public static void Error(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex);
        }

        public static void Error(string message)
        {
            _logger.Log(LogLevel.Error, message);
        }
        #endregion

        #region Warn
        public static void Warn(string message)
        {
            _logger.Warn(message);
        }
        #endregion
    }
}
