using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    private static EventSystem instance = null;
    public static EventSystem Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new EventSystem();
            return instance;
        }
    }
    public Dictionary<string, Action> EvenDir { get; private set; }//实际事件链
    public Dictionary<string, Action<object>> EvenDirOne { get; private set; }//实际事件链(带一个参数)
    public Dictionary<string, Action<object, object>> EvenDirTwo { get; private set; }//实际事件链(带两个参数)
    public Dictionary<object, Dictionary<string, Delegate>> EvenListiner { get; private set; }//记录表
    public EventSystem()
    {
        EvenDir = new Dictionary<string, Action>();
        EvenDirOne = new Dictionary<string, Action<object>>();
        EvenDirTwo = new Dictionary<string, Action<object, object>>();
        EvenListiner = new Dictionary<object, Dictionary<string, Delegate>>();
    }
    public void Add(object listner, String s, Action action)
    {
        //检测监听器是否存在
        //检测监听器记录本里的事件是否注册
        //检测事件链中是否加入
        if (listner == null)
            Debug.Log(s + "pre is null");
        if (!EvenListiner.ContainsKey(listner))
        {
            EvenListiner[listner] = new Dictionary<string, Delegate>();
        }
        if (EvenListiner[listner].ContainsValue(action))
        {
            Debug.Log(s + "this event have been reggiter");
            return;
        }
        EvenListiner[listner][s] = action;
        if (!EvenDir.ContainsKey(s))
        {
            Debug.Log(s + "初次注册成功");
            EvenDir[s] = action;
        }
        else
        {
            Debug.Log(s + "添加事件成功");
            EvenDir[s] += action;
        }

    }
    public void Add<T>(object listner, String s, Action<T> action)
    {
        //泛型带一个参数重载
        //检测监听器是否存在
        //检测监听器记录本里的事件是否注册
        //检测事件链中是否加入
        Action<object> act = (obj) => action((T)obj);
        if (listner == null)
            Debug.Log("pre is null");
        if (!EvenListiner.ContainsKey(listner))
        {
            EvenListiner[listner] = new Dictionary<string, Delegate>();
        }
        if (EvenListiner[listner].ContainsValue(action))
        {
            Debug.Log(s + "this event have been reggiter");
            return;
        }
        EvenListiner[listner][s] = act;
        if (!EvenDirOne.ContainsKey(s))
        {
            Debug.Log(s + "初次注册成功（带参）");
            EvenDirOne[s] = act;
        }
        else
        {
            Debug.Log(s + "添加事件成功（带参）");
            EvenDirOne[s] += act;
        }

    }
    public void Add<T, K>(object listner, String s, Action<T, K> action)
    {
        //泛型带一个参数重载
        //检测监听器是否存在
        //检测监听器记录本里的事件是否注册
        //检测事件链中是否加入
        Action<object, object> act = (obj1, obj2) => action((T)obj1, (K)obj2);
        if (listner == null)
            Debug.Log("pre is null");
        if (!EvenListiner.ContainsKey(listner))
        {
            EvenListiner[listner] = new Dictionary<string, Delegate>();
        }
        if (EvenListiner[listner].ContainsValue(action))
        {
            Debug.Log(s + "this event have been reggiter");
            return;
        }
        EvenListiner[listner][s] = act;
        if (!EvenDirTwo.ContainsKey(s))
        {
            Debug.Log(s + "初次注册成功（带参）");
            EvenDirTwo[s] = act;
        }
        else
        {
            Debug.Log(s + "添加事件成功（带参）");
            EvenDirTwo[s] += act;
        }

    }

    public void RemoveAll(object listner)
    {
        //同remove
        //利用foreach遍历获取的evens后逐个删除
        if (!EvenListiner.TryGetValue(listner, out var events))
            return;
        foreach (var e in events)
        {
            if (e.Value is Action a)//等价于if（e.value is(类型判断) Action）{  Action a=(Action)e.vallue;   }
            {
                if (EvenDir.ContainsKey(e.Key))
                    EvenDir[e.Key] -= a;
            }
            else if (e.Value is Action<object> ao)
            {
                if (EvenDirOne.ContainsKey(e.Key))
                    EvenDirOne[e.Key] -= ao;
            }
            else if (e.Value is Action<object, object> at)
            {
                if (EvenDirTwo.ContainsKey(e.Key))
                    EvenDirTwo[e.Key] += at;
            }
        }
        EvenListiner.Remove(listner);
    }
    public void OnTriggerEven(String s)
    {
        if (EvenDir.ContainsKey(s))
        {
            Debug.Log(s + "被触发");
            EvenDir[s]?.Invoke();
        }
        else
        {
            Debug.Log(s + "Even is null");
        }
    }
    public void OnTriggerEven(String s, object obj)
    {
        if (EvenDirOne.ContainsKey(s))
        {
            Debug.Log(s + "被触发带一参");
            EvenDirOne[s]?.Invoke(obj);
        }
        else
        {
            Debug.Log(s + "Evenone is null");
        }
    }
    public void OnTriggerEven(String s, object obj1, object obj2)
    {
        if (EvenDirTwo.ContainsKey(s))
        {
            Debug.Log(s + "被触发带二参");
            EvenDirTwo[s]?.Invoke(obj1, obj2);
        }
        else
        {
            Debug.Log(s + "Evenone is null");
        }
    }
}
