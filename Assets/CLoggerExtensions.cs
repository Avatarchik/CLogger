using System;

namespace caneva20.Logging {
    public static class CLoggerExtensions {
        public static void Debug(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Debug);
        }

        public static void Information(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Information);
        }

        public static void Warning(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Warning);
        }

        public static void Error(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Error);
        }

        public static void Error(this ICLogger logger, string message, Exception exception) {
            logger.Log(message, LogLevel.Error, exception);
        }

        public static void Error(this ICLogger logger, Exception exception) {
            logger.Log("", LogLevel.Error, exception);
        }
    }
}