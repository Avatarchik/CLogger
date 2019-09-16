using System;
using System.Collections.Generic;
using System.Linq;
using caneva20.ConfigAssets;
using UnityEditor;
using UnityEngine;
// ReSharper disable ConvertToAutoProperty

namespace caneva20.Logging.Management.Configurations {
    public class ManagerConfig : Config<ManagerConfig> {
        [SerializeField] private string _debugPrefix = "DEBUG";
        [SerializeField] private string _tracePrefix = "TRACE";
        [SerializeField] private List<string> _ignoredNamespaces;
        [SerializeField] private List<LoggerConfig> _configs;

        public string DebugPrefix => _debugPrefix;
        public string TracePrefix => _tracePrefix;
        public IEnumerable<string> IgnoredNamespaces => _ignoredNamespaces ?? Enumerable.Empty<string>();

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

            return _configs != null && _configs.Any(_ => _.Id == id);
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

        private void OnValidate() {
            var space = typeof(CLogger).Namespace;

            if (!_ignoredNamespaces.Contains(space)) {
                _ignoredNamespaces.Add(space);
            }
        }
    }
}