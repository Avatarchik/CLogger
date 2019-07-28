using caneva20.Logging.Management;
using UnityEditor;
using UnityEngine;

namespace caneva20.Logging.Editor {
    public static class EditorTools {

        [MenuItem("Tools/CLogger/Recreate loader")]
        public static void RecreateLoader() {
            Debug.Log($"Recreating scene's {nameof(CLoggerManagerLoader)}");
            
            ProjectSetuper.DestroyAllLoaders();
            ProjectSetuper.CreateManagerLoader();
        }
    }
}