using System;
using caneva20.Logging.Management.Configurations;
using UnityEngine;

namespace caneva20.Logging.Loggers {
    public class NativeLogger : ICLogger {
        private static string DebugTag => ManagerConfig.Instance.DebugPrefix;
        private static string TraceTag => ManagerConfig.Instance.TracePrefix;

        private readonly LoggerConfig _config;
        private string Tag => _config.Tag;

        public NativeLogger(LoggerConfig config) {
            _config = config;
        }

        private string GetTag(LogLevel level) {
            var tag = "";

            switch (level) {
                case LogLevel.Disabled: break;
                case LogLevel.Trace:
                    tag = $"{TraceTag}:{Tag}";
                    break;
                case LogLevel.Debug:
                    tag = $"{DebugTag}:{Tag}";
                    break;
                case LogLevel.Information:
                    tag = Tag;
                    break;
                case LogLevel.Warning:
                    tag = Tag;
                    break;
                case LogLevel.Error:
                    tag = Tag;
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