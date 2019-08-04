using System;
using System.Linq;
using caneva20.Logging.Management.Configurations;
using UnityEngine;

#if UNITY_EDITOR

namespace caneva20.Logging.Loggers {
    public class EditorLogger : ICLogger {
        private readonly string _id = GenerateId(5);

        private readonly LoggerConfig _config;

        private static string DebugTag => ManagerConfig.Instance.DebugPrefix;
        private static string TraceTag => ManagerConfig.Instance.TracePrefix;
        private string Tag => _config.Tag;

        public EditorLogger(LoggerConfig config) {
            _config = config;
        }

        private string GetTag(LogLevel level) {
            var color = "";
            var tag = "";

            switch (level) {
                case LogLevel.Disabled: break;
                case LogLevel.Trace:
                    color = "green";
                    tag = $"{TraceTag}:{Tag}";
                    break;
                case LogLevel.Debug:
                    tag = $"{DebugTag}:{Tag}";
                    color = "green";
                    break;
                case LogLevel.Information:
                    tag = Tag;
                    color = "blue";
                    break;
                case LogLevel.Warning:
                    tag = Tag;
                    color = "yellow";
                    break;
                case LogLevel.Error:
                    tag = Tag;
                    color = "red";
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
            
            return $"<color=white>{_id}</color> <color={color}>[{tag}]</color>";
        }

        public void Log(string message, LogLevel level, Exception exception = null) {
            if (_config.Level == LogLevel.Disabled || level < _config.Level) {
                return;
            }

            var tag = GetTag(level);

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
                    var logType = Application.GetStackTraceLogType(LogType.Log);
                    Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.None);

                    message += $"{(!string.IsNullOrWhiteSpace(message) ? "\n" : "")}{(exception != null ? exception.Message : "")}";
                    message += $"\n{exception?.StackTrace}";

                    Debug.LogError($"{tag} {message}");

                    Application.SetStackTraceLogType(LogType.Warning, logType);
                    break;
                case LogLevel.Disabled: break;
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        private static string GenerateId(int length) {
            var chars = Guid.NewGuid().ToString("N").Take(length);

            return string.Join("", chars).ToUpperInvariant();
        }
    }
}

#endif