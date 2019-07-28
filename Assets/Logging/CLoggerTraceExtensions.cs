using System;

namespace caneva20.Logging {
    public static class CLoggerTraceExtensions {
        public static void Trace(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Trace);
        }

        public static void Trace(this ICLogger logger, Action action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }
    }
}