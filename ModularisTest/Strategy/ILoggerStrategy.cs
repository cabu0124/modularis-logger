namespace ModularisTest.Strategy
{

    public interface ILoggerStrategy
    {
        void Log(LogType logType, string message);
    }

    public enum LogType
    {
        Message,
        Warning,
        Error
    }
}
