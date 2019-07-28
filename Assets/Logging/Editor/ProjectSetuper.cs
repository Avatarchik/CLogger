using System.Collections.Generic;
using System.Linq;
using caneva20.Logging.Management;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace caneva20.Logging.Editor {
    [InitializeOnLoad]
    public class ProjectSetuper {
        private const int EXECUTION_ORDER = -10000;

        static ProjectSetuper() {
            UpdateExecutionOrder();
            CreateManagerLoader();
        }

        private static void UpdateExecutionOrder() {
            var targetName = typeof(CLoggerManagerLoader).Name;

            var monoScripts = MonoImporter.GetAllRuntimeMonoScripts().Where(monoScript => monoScript.name == targetName);

            foreach (var script in monoScripts) {
                var currentOrder = MonoImporter.GetExecutionOrder(script);

                if (currentOrder != EXECUTION_ORDER) {
                    MonoImporter.SetExecutionOrder(script, EXECUTION_ORDER);
                }
            }
        }

        private static IEnumerable<CLoggerManagerLoader> FindLoadersInScene() {
            var rootObjs = SceneManager.GetActiveScene().GetRootGameObjects();

            return rootObjs.Where(_ => _.GetComponent<CLoggerManagerLoader>() != null).Select(_ => _.GetComponent<CLoggerManagerLoader>());
        }

        public static void CreateManagerLoader() {
            var loaders = FindLoadersInScene().ToList();
            
            if (loaders.Count > 1) {
                DestroyAllLoaders();
                
                loaders = new List<CLoggerManagerLoader>();
            }
            
            if (loaders.Count <= 0) {
                CreateLoader();
            }
        }

        private static void CreateLoader() {
            Debug.Log($"Creating new {nameof(CLoggerManagerLoader)}");
            
            var loader = new GameObject(nameof(CLoggerManagerLoader));

            loader.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
            loader.AddComponent<CLoggerManagerLoader>();
        }

        public static void DestroyAllLoaders() {
            var loaders = FindLoadersInScene().ToList();

            for (var i = loaders.Count - 1; i >= 0; i--) {
                Object.DestroyImmediate(loaders[i].gameObject);
            }
        }
    }
}