using ModularisTest.Exceptions;

namespace ModularisTest.Strategy
{
    public class DatabaseLoggerStrategy : ILoggerStrategy
    {
        public void Log(LogType logType, string message)
        {
            throw new CustomLoggerException(ErrorMessages.LoggerStrategy.Database.DatabaseLogError);
        }
    }
}
