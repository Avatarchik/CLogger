using System;
using caneva20.Logging.Management.Configurations;

namespace caneva20.Logging.Exceptions {
    public class InvalidFieldInfoException : Exception {
        public InvalidFieldInfoException() 
            : base($"Field is not of type {StaticConfig.LoggerType.Name} nor is assignable from it") {
        }
    }
}