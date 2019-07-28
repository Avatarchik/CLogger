using System;
using System.Reflection;

namespace caneva20.Logging.Management.Configurations {
    internal static class StaticConfig {
        internal static readonly string[] IgnoredAssemblies = {
            "UnityEngine",
            "Google",
            "Microsoft",
            "JetBrains",
            "System",
            "UnityEditor",
            "netstandard",
            "Unity",
            "mscorlib",
            "Mono",
            "nunit",
        };
        
        internal const BindingFlags FLAGS = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        internal static readonly Type LoggerType = typeof(ICLogger);
    }
}