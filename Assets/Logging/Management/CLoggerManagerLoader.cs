using UnityEngine;

namespace caneva20.Logging.Management {
    public class CLoggerManagerLoader : MonoBehaviour {
        private void Awake() {
            LoggerManager.Initialize();
        }
    }
}