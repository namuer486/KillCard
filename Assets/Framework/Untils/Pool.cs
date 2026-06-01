using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable//对象池对象需要继承此接口
{
    public void OnGet();
    public void OnBack();
}
public class Pool<T> where T : MonoBehaviour//泛型限定为继承自MonoBehaviour
{
    public Stack<T> stack {  get; private set; }
    public T Prefab { get; private set; }
    public Transform Parent { get; private set; }
    public Pool(T Prefab, int count = 0, Transform Parent = null)
    {
        stack = new Stack<T>();
        this.Prefab = Prefab;
        this.Parent = Parent;
        for (int i = 0; i < count; i++)
        {
            T obj = Create();
            obj.gameObject.SetActive(false);
            stack.Push(obj);
        }
    }
    private T Create()
    {
        T obj = Object.Instantiate<T>(Prefab, Parent);
        return obj;
    }
    public T Get()
    {
        if(stack.Count <= 0)
        {
            Debug.LogError(this + "对象池空了");
            return null;
        }
        T obj = stack.Pop();
        if (obj is IPoolable poolable)
        {
            poolable.OnGet();
        }
        return obj;
    }
    public void Back(T obj)
    {
        if (obj == null)
        {
            Debug.LogError(this + "空对象回池");
        }
        if(obj is IPoolable poolable)
        {
            poolable.OnBack();
        }
        obj.gameObject.SetActive(false);
        stack.Push(obj);
    }
    public void Clear()
    {
        if (stack.Count <= 0)
        {
            return;
        }
        foreach (var obj in stack)
        {
            if (obj)
            {
                Object.Destroy(obj);
            }
        }
        stack.Clear();
    }
}
