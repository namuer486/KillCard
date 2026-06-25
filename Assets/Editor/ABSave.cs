using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ABSave:MonoBehaviour
{
    public static string path = "Assets/StreamingAssets/AB";
    [MenuItem("AB/ABSave")]
    public static void AssetBundleSave()
    {
        BuildPipeline.BuildAssetBundles(path,BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows);
    }
}
