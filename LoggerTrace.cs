using System;

namespace caneva20.Logging {
    public static partial class Logger {
        public static void Trace(string msg) {
            Get().Log(msg, LogLevel.Trace);
        }

        public static void Trace(Action action) {
            Get().Log(action.Method.Name, LogLevel.Trace);
        }
    }
}