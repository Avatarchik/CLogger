using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
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
            var config = new LoggerConfig(field);
                    
            _configs.Add(config);

        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
        #endif

            return config;
        }

        public void Remove(FieldInfo field) {
            if (!HasConfig(field, out var config)) {
                return;
            }

            _configs.Remove(config);
        }
    }
}