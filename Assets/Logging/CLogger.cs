using System;
using System.Diagnostics;
using caneva20.Logging.Loggers;
using caneva20.Logging.Management.Configurations;

namespace caneva20.Logging {
    public static class CLogger {
        private const int FRAME_INDEX = 2;

        public static ICLogger Create() {
            var config = ManagerConfig.Instance[GetDeclaringType()]; 

            return Create(config);
        }

        private static Type GetDeclaringType() {
            return new StackTrace().GetFrame(FRAME_INDEX).GetMethod().DeclaringType;
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