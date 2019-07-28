using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using caneva20.Logging.Management.Configurations;

namespace caneva20.Logging.Management {
    internal static class LoggerLoader {
        public static IEnumerable<FieldInfo> FindLoggers() {
            return FindAssemblies().SelectMany(assembly => assembly.GetTypes().SelectMany(FindFields).ToList());
        }

        private static IEnumerable<Assembly> FindAssemblies() {
            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !IsIgnored(assembly));
        }

        private static bool IsIgnored(Assembly assembly) {
            var assemblyName = assembly.GetName().Name;
            
            return StaticConfig.IgnoredAssemblies.Any(ignoredName => assemblyName.Contains(ignoredName));
        }

        private static IEnumerable<FieldInfo> FindFields(Type type) {
            return type.GetFields(StaticConfig.FLAGS).Where(_ => StaticConfig.LoggerType.IsAssignableFrom(_.FieldType));
        }
    }
}