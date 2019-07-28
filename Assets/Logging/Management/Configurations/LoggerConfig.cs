using System;
using System.Reflection;
using UnityEngine;

namespace caneva20.Logging.Management.Configurations {
    [Serializable]
    public class LoggerConfig {
        [SerializeField] private string _loggerId;
        
        [SerializeField] private LogLevel _logLevel;
        [SerializeField] private string _tag;

        public LoggerConfig(string id) {
            _loggerId = id;
        }
        
        public string Id => _loggerId;

        public LogLevel Level {
            get => _logLevel;
            set => _logLevel = value;
        }

        public string Tag {
            get => _tag;
            set => _tag = value;
        }
    }
}