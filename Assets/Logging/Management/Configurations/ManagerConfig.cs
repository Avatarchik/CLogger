using System;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private string _debugPrefix = "DEBUG";
        [SerializeField] private string _tracePrefix = "TRACE";
        [SerializeField] private List<LoggerConfig> _configs;

        public string DebugPrefix => _debugPrefix;
        public string TracePrefix => _tracePrefix;

        public LoggerConfig this[Type type] => GetConfig(type);

        public LoggerConfig GetConfig(Type type) {
            var id = GetIdFromType(type);

            if (!HasConfig(type)) {
                Debug.Log($"Config not found for {id}");
                return Add(type);
            }

            return _configs.FirstOrDefault(_ => _.Id == id);
        }

        private bool HasConfig(Type type) {
            var id = GetIdFromType(type);

            return _configs.Any(_ => _.Id == id);
        }

        public LoggerConfig Add(Type type) {
            var config = new LoggerConfig(type);

            _configs.Add(config);

        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
        #endif

            return config;
        }

        public void Remove(Type type) {
            if (!HasConfig(type)) {
                return;
            }

            _configs.Remove(GetConfig(type));
        }

        private static string GetIdFromType(Type type) => LoggerConfig.GetId(type);
    }
}