using System.Reflection;
using caneva20.Logging.Management.Configurations;
using UnityEngine;

namespace caneva20.Logging.Management {
    internal static class LoggerManager {
        public static void Initialize() {
            var loggers = LoggerLoader.FindLoggers();
            
            
        }

        private static void InitializeLogger(FieldInfo field) {
            var config = ManagerConfig.Instance[field];
            
            
        }
    }
}