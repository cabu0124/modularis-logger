namespace ModularisTest.Exceptions
{
    public static class ErrorMessages
    {
        public readonly struct LoggerStrategy
        {
            public static readonly string MessageNullOrEmpty = "The message is null or empty.";

            public readonly struct Console
            {
                public static readonly string ConsoleLogError = "Error creating Console Log.";
            }

            public readonly struct File
            {
                public static readonly string FileLogError = "Error creating File Log.";
            }

            public readonly struct Database
            {
                public static readonly string DatabaseLogError = "Error creating Database Log.";
            }
        }
    }
}
