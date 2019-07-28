using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace caneva20.Logging.Management.Configurations {
    public class ManagerConfig : ScriptableObject {
        private static ManagerConfig _instance;

        public static ManagerConfig Instance {
            get {
                if (_instance == null) {
                    _instance = ManagerConfigLoader.Load();
                    
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        [SerializeField] private List<LoggerConfig> _configList;

        private bool _initialized;
        private readonly Dictionary<string, LoggerConfig> _configs = new Dictionary<string, LoggerConfig>();

        private void Initialize() {
            if (_initialized) {
                return;
            }

            foreach (var config in _configList) {
                _configs.Add(config.Id, config);
            }

            _initialized = true;
        }

        private bool HasConfig(Type type) {
            Initialize();

            var id = GetIdFromType(type);

            return _configs.ContainsKey(id);
        }

        public LoggerConfig this[Type type] => GetConfig(type);

        public LoggerConfig GetConfig(Type type) {
            if (!HasConfig(type)) {
                return Add(type);
            }

            return _configs[GetIdFromType(type)];
        }

        public LoggerConfig Add(Type type) {
            var config = new LoggerConfig(GetIdFromType(type));

            _configList.Add(config);
            _configs.Add(config.Id, config);

        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
        #endif

            return config;
        }

        public void Remove(Type type) {
            if (!HasConfig(type)) {
                return;
            }
            
            _configList.Remove(GetConfig(type));
            _configs.Remove(GetIdFromType(type));
        }

        private static string GetIdFromType(Type type) {
            return type.FullName;
        }
    }
}