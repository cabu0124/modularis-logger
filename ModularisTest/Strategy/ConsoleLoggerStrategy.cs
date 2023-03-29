using ModularisTest.Exceptions;
using System;

namespace ModularisTest.Strategy
{
    public class ConsoleLoggerStrategy : ILoggerStrategy
    {
        public void Log(LogType logType, string message)
        {
            try
            {
                message = message.Trim();
                switch (logType)
                {
                    case LogType.Message:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            }
            catch (Exception ex)
            {
                throw new CustomLoggerException(ErrorMessages.LoggerStrategy.Console.ConsoleLogError, ex.InnerException);
            }
            
        }
    }
}
