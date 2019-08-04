using System;

namespace caneva20.Logging {
    public static partial class Logger {
        public static void Debug(string msg) {
            Get().Log(msg, LogLevel.Debug);
        }

        public static void Info(string msg) {
            Get().Log(msg, LogLevel.Information);
        }

        public static void Warn(string msg) {
            Get().Log(msg, LogLevel.Warning);
        }

        public static void Error(string msg) {
            Get().Log(msg, LogLevel.Error);
        }

        public static void Error(Exception exception) {
            Get().Log(null, LogLevel.Error, exception);
        }

        public static void Error(string msg, Exception exception) {
            Get().Log(msg, LogLevel.Error, exception);
        }
    }
}