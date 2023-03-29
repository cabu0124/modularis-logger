using ModularisTest.Exceptions;
using ModularisTest.Strategy;
using System.Collections.Generic;

namespace ModularisTest
{
    public class JobLogger
    {
        private readonly IEnumerable<ILoggerStrategy> _strategies;

        public JobLogger(IEnumerable<ILoggerStrategy> strategies)
        {
            _strategies = strategies;
        }

        public void LogMessage(LogType logType, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new CustomLoggerException(ErrorMessages.LoggerStrategy.MessageNullOrEmpty);
            }

            foreach (var strategy in _strategies)
            {
                strategy.Log(logType, message);
            }
        }
    }
}