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
    }
}