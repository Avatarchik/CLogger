using System;

namespace caneva20.Logging {
    public interface ICLogger {
        void Log(string message, LogLevel level, Exception exception = null);
    }
}