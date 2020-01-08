using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Utils
{
    public class LogUtil
    {
        private string logFileName; // 
        private string logFilePath; //

        private NLog.Config.LoggingConfiguration config; 
        private NLog.Targets.FileTarget fileTarget;
        
        public LogUtil()
        {
            this.logFileName = DateTime.Now.Date.ToString() + "-server-log.txt";
            this.logFilePath = @"Logs\" + this.logFileName;

            this.config = new NLog.Config.LoggingConfiguration();
            fileTarget = new NLog.Targets.FileTarget("logfile")
            {
                FileName = this.logFilePath,
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            this.config.AddTarget(this.fileTarget);
            this.config.AddRuleForAllLevels(this.fileTarget);
            NLog.LogManager.Configuration = this.config;
        }

        private NLog.Logger nLog = NLog.LogManager.GetCurrentClassLogger();

        public void addLog(string logLevel, string logMessage, Exception exception = null)
        {
            if(exception == null)
            {
                switch (logLevel)
                {
                    case "info":
                        nLog.Info(logMessage);
                        break;
                    case "warn":
                        nLog.Warn(logMessage);
                        break;
                    case "debug":
                        nLog.Debug(logMessage);
                        break;
                    case "error":
                        nLog.Error(logMessage);
                        break;
                    case "fatal":
                        nLog.Fatal(logMessage);
                        break;
                    default:
                        nLog.Error(logMessage);
                        break;
                }
            }
            else
            {
                switch (logLevel)
                {
                    case "info":
                        nLog.Info(exception, logMessage);
                        break;
                    case "warn":
                        nLog.Warn(exception, logMessage);
                        break;
                    case "debug":
                        nLog.Debug(exception, logMessage);
                        break;
                    case "error":
                        nLog.Error(exception, logMessage);
                        break;
                    case "fatal":
                        nLog.Fatal(exception, logMessage);
                        break;
                    default:
                        nLog.Error(exception, logMessage);
                        break;
                }
            }
        }
    }
}