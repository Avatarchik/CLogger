using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR

namespace caneva20.Logging.Loggers {
    public class EditorLogger : ICLogger {
        private readonly string _id = GenerateId(5);

        private const string DEBUG_TAG = "DEBUG";
        private const string TRACE_TAG = "TRACE";
        private readonly string _defaultTag;
        private readonly LogLevel _minLevel;

        public EditorLogger(string defaultTag, LogLevel minLevel) {
            _defaultTag = defaultTag;
            _minLevel = minLevel;
        }

        private string GetTag(LogLevel level) {
            var color = "";
            var tag = "";

            switch (level) {
                case LogLevel.Disabled: break;
                case LogLevel.Trace:
                    color = "green";
                    tag = $"{TRACE_TAG}:{_defaultTag}";
                    break;
                case LogLevel.Debug:
                    tag = $"{DEBUG_TAG}:{_defaultTag}";
                    color = "green";
                    break;
                case LogLevel.Information:
                    tag = _defaultTag;
                    color = "blue";
                    break;
                case LogLevel.Warning:
                    tag = _defaultTag;
                    color = "yellow";
                    break;
                case LogLevel.Error:
                    tag = _defaultTag;
                    color = "red";
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            return $"<color=white>{_id}</color> <color={color}>[{tag}]</color>";
        }

        public void Log(string message, LogLevel level, Exception exception = null) {
            if (_minLevel == LogLevel.Disabled || level < _minLevel) {
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