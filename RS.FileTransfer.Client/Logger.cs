using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Client
{
    public class Logger
    {
        NLog.Logger _NLogger = null;

        public Logger()
        {

            // Step 1. Create configuration object

            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = "${basedir}/Log.txt";
            //fileTarget.FileName = "c:\\WOLRelaylog.txt";
            fileTarget.ArchiveFileName = "WOLRelaylog_{{#####}}.txt";
            fileTarget.ArchiveAboveSize = 1000000;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            fileTarget.Layout = "${longdate} ${level} ${message}";
            fileTarget.ConcurrentWrites = true;
            fileTarget.KeepFileOpen = false;
            fileTarget.Encoding = Encoding.UTF8;
            LoggingRule rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            // Example usage

            _NLogger = LogManager.GetLogger("Log");
            
            /*


            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.Name = "eiPower WOL Relay Saver Target";
            fileTarget.Layout = "${longdate} ${level} ${message}";
            fileTarget.ArchiveAboveSize = 1000000;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            fileTarget.ConcurrentWrites = true;
            fileTarget.KeepFileOpen = false;
            fileTarget.Encoding = Encoding.UTF8;
            fileTarget.FileName = "c:\\WOLRelaylog.txt";
            fileTarget.pa
            fileTarget.ArchiveFileName = "WOLRelaylog_{{#####}}.txt";

            LoggingRule rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            _NLogger = LogManager.GetLogger("Logger");
             * */
        }

        public void LogError(string message)
        {
            _NLogger.Error(message);
        }

        public void LogWarning(string message)
        {
            _NLogger.Warn(message);
        }

        public void LogInformation(string message)
        {
            _NLogger.Info(message);
        }
    }
}
