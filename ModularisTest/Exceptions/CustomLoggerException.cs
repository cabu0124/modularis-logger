using System;

namespace ModularisTest.Exceptions
{
    public class CustomLoggerException : Exception
    {
        public CustomLoggerException(string message, Exception innerException = null) : base ( message, innerException){}
    }
}