using System;
using System.Collections.Generic;
using System.Diagnostics;
using caneva20.Logging.Loggers;
using caneva20.Logging.Management.Configurations;

namespace caneva20.Logging {
    public static partial class CLogger {
        private const int FRAME_INDEX = 3;
        private static readonly Dictionary<Type, ICLogger> _loggers = new Dictionary<Type, ICLogger>();

        private static Type GetDeclaringType() {
            return new StackTrace().GetFrame(FRAME_INDEX).GetMethod().DeclaringType;
        }

        private static ICLogger Get() {
            var type = GetDeclaringType();

            if (_loggers.ContainsKey(type)) {
                return _loggers[type];
            }

            var logger = Create(ManagerConfig.Instance[type]);
            
            _loggers.Add(type, logger);

            return logger;
        }
        
        private static ICLogger Create(LoggerConfig config) {
        #if UNITY_EDITOR
            return new EditorLogger(config);
        #else
            return new NativeLogger(config);
        #endif
        }
    }
}