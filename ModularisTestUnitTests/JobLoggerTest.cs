using ModularisTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModularisTest.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using ModularisTest.Exceptions;

namespace ModularisTestUnitTests
{
    [TestClass]
    public class JobLoggerTest
    {
        private const string TestMessage = "Test Message";
        private const string WarningMessage = "Test Warning";
        private const string ErrorMessage = "Test Error";
        private const string ExceptionMessageNullOrEmpty = "The message is null or empty.";
        private const string ExceptionDatabaseLogError = "Error creating Database Log.";

        private readonly Mock<ILoggerStrategy> loggerStrategyMock = new Mock<ILoggerStrategy>();
        private readonly JobLogger jobLogger;

        public JobLoggerTest() {
            jobLogger = new JobLogger(new List<ILoggerStrategy> { loggerStrategyMock.Object});
        }

        [TestMethod]
        public void JobLoggerBasicTest()
        {
            jobLogger.LogMessage(LogType.Message, TestMessage);
            jobLogger.LogMessage(LogType.Warning, WarningMessage);
            jobLogger.LogMessage(LogType.Error, ErrorMessage);

            loggerStrategyMock.Verify(x => x.Log(It.IsAny<LogType>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [TestMethod]
        public void JobLoggerMessageNullErrorTest()
        {
            var result = Assert.ThrowsException<CustomLoggerException>(() => jobLogger.LogMessage(LogType.Message, null));
            Assert.AreEqual(ExceptionMessageNullOrEmpty, result.Message);
        }

        [TestMethod]
        public void JobLoggerMessageWithSpacesTest()
        {
            jobLogger.LogMessage(LogType.Message, "      Text with spaces    ");

            loggerStrategyMock.Verify(x => x.Log(It.IsAny<LogType>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void JobLoggerDependencyInjectionTest()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerStrategy>(new ConsoleLoggerStrategy());
            services.AddSingleton<ILoggerStrategy>(new FileLoggerStrategy());
            services.AddSingleton<JobLogger>();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<JobLogger>();
            logger.LogMessage(LogType.Message, TestMessage);
            logger.LogMessage(LogType.Warning, WarningMessage);
            logger.LogMessage(LogType.Error, ErrorMessage);
        }

        [TestMethod]
        public void JobLoggerStrategyNotImplementedTest()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerStrategy>(new DatabaseLoggerStrategy());
            services.AddSingleton<JobLogger>();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<JobLogger>();
            var result = Assert.ThrowsException<CustomLoggerException>(() => logger.LogMessage(LogType.Message, TestMessage));
            Assert.AreEqual(ExceptionDatabaseLogError, result.Message);
        }
    }
}