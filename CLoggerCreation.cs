using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using caneva20.Logging.Loggers;
using caneva20.Logging.Management.Configurations;

namespace caneva20.Logging {
    public static partial class CLogger {
        private static readonly Dictionary<Type, ICLogger> _loggers = new Dictionary<Type, ICLogger>();

        private static Type GetDeclaringType() {
            var currentFrame = 0;
            var stack = new StackTrace();
		
            var root = NextType();
		
            while (root != null && !IsValidType(root)) {
                root = NextType();
            }
		
            if(root == null) {
                throw new Exception("No valid root type was found");
            }
		
            return root;
		
            Type NextType() {
                return stack.GetFrame(currentFrame++).GetMethod().DeclaringType;
            }
        }

        private static bool IsValidType(Type type) {
            Console.WriteLine($"Checking {type.Name}");

            return !IsIgnored(type) && !IsAnonymousType(type);
        }

        private static bool IsIgnored(Type type) {
            return type?.Namespace != null && ManagerConfig.Instance.IgnoredNamespaces.Any(s => type.Namespace.StartsWith(s));
        }

        private static bool IsAnonymousType(Type type) {
            var isCompilerGenerated = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
            var hasInvalidName = type.Name.Contains("<") || type.Name.Contains(">");

            return isCompilerGenerated || hasInvalidName;
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