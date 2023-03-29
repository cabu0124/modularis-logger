using System.Configuration;
using System;
using System.IO;
using ModularisTest.Exceptions;

namespace ModularisTest.Strategy
{
    public class FileLoggerStrategy : ILoggerStrategy
    {
        public void Log(LogType logType, string message)
        {
            try
            {
                message = message.Trim();
                string logFileContent = string.Empty;

                var logFolder = ConfigurationManager.AppSettings["LogFileDirectory"];
                if (string.IsNullOrEmpty(logFolder)) logFolder = Environment.CurrentDirectory;
                var logFileName = string.Format("LogFile{0}.txt", DateTime.Now.ToShortDateString().Replace("/", "."));
                var logFullFilePath = Path.Combine(logFolder, logFileName);

                if (File.Exists(logFullFilePath))
                {
                    logFileContent = File.ReadAllText(logFullFilePath);
                }

                logFileContent += string.Format("{0} {1} {2}{3}", DateTime.Now.ToShortDateString(), Enum.GetName(typeof(LogType), logType), message, Environment.NewLine);
                if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);

                File.WriteAllText(logFullFilePath, logFileContent);
            }
            catch (Exception ex)
            {
                throw new CustomLoggerException(ErrorMessages.LoggerStrategy.File.FileLogError, ex.InnerException);
            }
        }
    }
}
