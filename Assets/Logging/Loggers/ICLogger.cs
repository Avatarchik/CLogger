using System;

namespace caneva20.Logging.Loggers {
    public interface ICLogger {
        void Log(string message, LogLevel level, Exception exception = null);
    }
}