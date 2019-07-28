using System;
using UnityEngine;

namespace caneva20.Logging.Loggers {
    public class NativeLogger : ICLogger {
        private const string DEBUG_TAG = "DEBUG";
        private const string TRACE_TAG = "TRACE";
        private readonly string _defaultTag;

        public NativeLogger(string defaultTag) {
            _defaultTag = defaultTag;
        }

        private string GetTag(LogLevel level) {
            var tag = "";

            switch (level) {
                case LogLevel.Disabled: break;
                case LogLevel.Trace:
                    tag = $"{TRACE_TAG}:{_defaultTag}";
                    break;
                case LogLevel.Debug:
                    tag = $"{DEBUG_TAG}:{_defaultTag}";
                    break;
                case LogLevel.Information:
                    tag = _defaultTag;
                    break;
                case LogLevel.Warning:
                    tag = _defaultTag;
                    break;
                case LogLevel.Error:
                    tag = _defaultTag;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            return $"[{tag}]";
        }
        
        public void Log(string message, LogLevel level, Exception exception = null) {
            if (!Debug.isDebugBuild) {
                return;
            }

            var tag = GetTag(level);

            var logType = Application.GetStackTraceLogType(LogType.Log);
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);

            switch (level) {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                    Debug.Log($"{tag} {message}");
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning($"{tag} {message}");
                    break;
                case LogLevel.Error:
                    message += $"{(!string.IsNullOrWhiteSpace(message) ? "\n" : "")}{(exception != null ? exception.Message : "")}";
                    message += $"\n{exception?.StackTrace}";

                    Debug.LogError($"{tag} {message}");
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            Application.SetStackTraceLogType(LogType.Log, logType);
        }
    }
}