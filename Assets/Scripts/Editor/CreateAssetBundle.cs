using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Editor.ScriptableObjects
{
    public class CreateAssetBundle : UnityEditor.Editor
    {
        [MenuItem("Assets/Create Asset Bundle")]
        public static void BuildAssetBundle()
        {
            string outputDirectoryPath = Application.dataPath + "/.AssetBundles/" + EditorUserBuildSettings.activeBuildTarget;

            if (!Directory.Exists(outputDirectoryPath))
                Directory.CreateDirectory(outputDirectoryPath);

            try
            {
                BuildPipeline.BuildAssetBundles(outputDirectoryPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
            }
        }
    }
}
