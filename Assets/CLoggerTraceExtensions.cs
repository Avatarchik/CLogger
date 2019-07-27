using System;

namespace caneva20.Logging {
    public static class CLoggerTraceExtensions {
        public static void Trace(this ICLogger logger, string message) {
            logger.Log(message, LogLevel.Trace);
        }

        public static void Trace(this ICLogger logger, Action action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<T1>(this ICLogger logger, Action<T1> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<T1, T2>(this ICLogger logger, Action<T1, T2> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<T1, T2, T3>(this ICLogger logger, Action<T1, T2, T3> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<T1, T2, T3, T4>(this ICLogger logger, Action<T1, T2, T3, T4> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<T1, T2, T3, T4, T5>(this ICLogger logger, Action<T1, T2, T3, T4, T5> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult>(this ICLogger logger, Func<TResult> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult, T1>(this ICLogger logger, Func<TResult, T1> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult, T1, T2>(this ICLogger logger, Func<TResult, T1, T2> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult, T1, T2, T3>(this ICLogger logger, Func<TResult, T1, T2, T3> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult, T1, T2, T3, T4>(this ICLogger logger, Func<TResult, T1, T2, T3, T4> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }

        public static void Trace<TResult, T1, T2, T3, T4, T5>(this ICLogger logger, Func<TResult, T1, T2, T3, T4, T5> action) {
            logger.Log(action.Method.Name, LogLevel.Trace);
        }
    }
}