using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
public static class ABConfig
{
    public static string Card = "card";
    //public static string Prefab = "card";
    public static string Normal = "normal";
    public static string Character = "character";
    public static string Map = "map";
    public static string Menu = "menu";
    public static string Table = "table";
}
public sealed class ResourseManager//修改资源加载，添加包缓存
{
    private static ResourseManager instance = null;
    public static ResourseManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = new ResourseManager();
            return instance;
        }
    }
    public Dictionary<string, AssetBundle> abs = new Dictionary<string, AssetBundle>();
    
    public T ResourcesLoad<T>(string abpath,string path) where T : UnityEngine.Object//需要载入的包名，名字
    {
        AssetBundle bundle = GetAssetsBuild(abpath);
        if (bundle == null)
            return null;
        Type type = typeof(T);
        T acsses = null;
        if (type.IsSubclassOf(typeof(MonoBehaviour)))
        {
            acsses = bundle.LoadAsset<GameObject>(path).GetComponent<T>();
        }
        else
        {
            acsses = bundle.LoadAsset<T>(path);
        }         
        //T acsses = Resources.Load<T>(path);
        if (acsses == null)
        {
            Debug.LogError("无法找到" + path + "资源");
            SearchAB(bundle);
        }
        return acsses;
    }
    public IEnumerable ResourcesLoadAsync<T>(string abpath, string path,Action<T> action) where T : UnityEngine.Object//异步加载
    {
        AssetBundle bundle = GetAssetsBuild(abpath);
        if(bundle == null)
        {
            Debug.LogError($"不存在{abpath}");
            action?.Invoke(null);
            yield break;
        }
        AssetBundleRequest req = bundle.LoadAssetAsync<T>(path);
        yield return req;
        T acsses = req.asset as T;
        if (acsses == null)
        {
            Debug.LogError($"{path}资源加载失败");
        }
        action?.Invoke(acsses);
    }
    public AssetBundle GetAssetsBuild(string abpath)
    {
        if(abs.TryGetValue(abpath, out AssetBundle bundle))
        {
            return bundle;
        }
        AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AB", abpath));
        if(ab != null)
        {
            abs[abpath] = ab;
        }
        return ab;
    }
    public void UnLoadAssetsBuild(string abpath)
    {
        if(abs.TryGetValue(abpath,out AssetBundle bundle))
        {
            bundle.Unload(false);
            abs.Remove(abpath);
        }
    }
    public void UnLoadAllAssetsBuild()
    {
        foreach(var b in abs)
        {
            b.Value.Unload(false);
        }
        abs.Clear();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
    public void SearchAB(AssetBundle ab)
    {
        string[] allPaths = ab.GetAllAssetNames();
        foreach (var p in allPaths) Debug.Log(p);
    }
}
