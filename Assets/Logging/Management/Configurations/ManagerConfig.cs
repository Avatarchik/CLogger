using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using caneva20.Logging.Exceptions;
using UnityEngine;

namespace caneva20.Logging.Management.Configurations {
    public class ManagerConfig : ScriptableObject {
        private static ManagerConfig _instance;
        
        public static ManagerConfig Instance {
            get {
                if (_instance == null) {
                    _instance = ManagerConfigLoader.Load();
                }

                return _instance;
            }
        }

        [SerializeField] private List<LoggerConfig> _configs;

        public LoggerConfig this[FieldInfo field] {
            get {
                ThrowIfNotLogger(field);

                if (!HasConfig(field, out var config)) {
                    config = Add(field);
                }
                
                return config;
            }
        }

        public bool HasConfig(FieldInfo field, out LoggerConfig config) {
            var fieldName = field.Name;
            var declaringTypeFullName = field.DeclaringType?.FullName;

            if (_configs == null) {
                _configs = new List<LoggerConfig>();
            }
            
            config = _configs.SingleOrDefault(_ => _.FieldName == fieldName && _.DeclaringTypeFullName == declaringTypeFullName);

            return config != null;
        }

        public LoggerConfig Add(FieldInfo field) {
            ThrowIfNotLogger(field);
            
            var config = new LoggerConfig(field);
                    
            _configs.Add(config);

        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
        #endif

            return config;
        }

        public void Remove(FieldInfo field) {
            ThrowIfNotLogger(field);

            if (!HasConfig(field, out var config)) {
                return;
            }

            _configs.Remove(config);
        }

        private static void ThrowIfNotLogger(FieldInfo field) {
            if (IsLogger(field)) {
                return;
            }
            
            throw new InvalidFieldInfoException();
        }
        
        private static bool IsLogger(FieldInfo field) {
            return StaticConfig.LoggerType.IsAssignableFrom(field.FieldType);
        }
    }
}