using System.IO;
using UnityEditor;
using UnityEngine;

namespace caneva20.Logging.Management.Configurations {
    public static class ManagerConfigLoader {
        public static ManagerConfig Load() {
            var config = Object.FindObjectOfType<ManagerConfig>();

            return config ? config : CreateConfig();
        }

        private static ManagerConfig CreateConfig() {
            var config = ScriptableObject.CreateInstance<ManagerConfig>();

        #if UNITY_EDITOR
            const string DIR_PATH = "Tools/CLogger/";
            const string ASSET_NAME = "CLoggerConfig.asset";

            var absolutePath = Path.Combine(Application.dataPath, DIR_PATH);

            if (!Directory.Exists(absolutePath)) {
                Directory.CreateDirectory(absolutePath);
            }

            AssetDatabase.CreateAsset(config, $"Assets/{DIR_PATH}{ASSET_NAME}");

            AssetDatabase.Refresh();
        #endif

            return config;
        }
    }
}