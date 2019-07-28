using System;
using System.Reflection;
using UnityEngine;

namespace caneva20.Logging.Management.Configurations {
    [Serializable]
    public class LoggerConfig {
        [SerializeField] private string _declaringTypeFullName;
        [SerializeField] private string _fieldName;
        
        [SerializeField] private LogLevel _logLevel;
        [SerializeField] private string _tag;

        public LoggerConfig(FieldInfo field) {
            _declaringTypeFullName = field.DeclaringType?.FullName;
            _fieldName = field.Name;
        }
        
        public string DeclaringTypeFullName => _declaringTypeFullName;
        public string FieldName => _fieldName;

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