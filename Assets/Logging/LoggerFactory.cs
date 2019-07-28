using System;
using caneva20.Logging.Loggers;

namespace caneva20.Logging {
    public static class LoggerFactory {
        public static ICLogger Create<T>(LogLevel minLevel) {
            return Create(typeof(T), minLevel);
        }

        public static ICLogger Create(Type type, LogLevel minLevel) {
            return Create(type.Name, minLevel);
        }
        
        public static ICLogger Create(string defaultTag, LogLevel minLevel) {
        #if UNITY_EDITOR
            return new EditorLogger(defaultTag, minLevel);
        #else
            return new NativeLogger(defaultTag);
        #endif
        }
    }
}