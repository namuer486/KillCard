using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourseManager
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

    public T ResourcesLoad<T>(string path) where T : UnityEngine.Object
    {
        T acsses = Resources.Load<T>(path);
        if (acsses == null)
        {
            Debug.LogError("无法找到资源");
        }
        return acsses;
    }
    public IEnumerable ResourcesLoadAsync<T>(string path,Action<T> action) where T : UnityEngine.Object//异步加载
    {
        ResourceRequest req = Resources.LoadAsync<T>(path);
        yield return req;
        T acsses = req.asset as T;
        if (acsses == null)
        {
            Debug.LogError("异步加载失败");
        }
        action?.Invoke(acsses);
    }
}
