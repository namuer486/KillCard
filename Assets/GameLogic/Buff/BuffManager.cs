using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    private static BuffManager instance;
    public static BuffManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            instance = new BuffManager();
            return instance;
        }
    }
    public List<Charocter> actors {  get; private set; }= new List<Charocter>();
    public Dictionary<Charocter, List<Buff>> BuffDic { get; private set; } = new Dictionary<Charocter, List<Buff>>();
    public BuffManager()
    {
    }
    public void AddBuff(Charocter actor, Buff buff)
    {
        if(!BuffDic.ContainsKey(actor))
        {
            BuffDic[actor] = new List<Buff>();
            BuffDic[actor].Add(buff);
            actors.Add(actor);
        }
        else
        {
            var bufflis = BuffDic[actor];
            bufflis.Add(buff);
        }
        buff.AddBuff(actor);
        if(actor is Player)
        {
            FrameworkCore.Event.OnTriggerEven("UpDetePlayerBuffUI", BuffDic[actor]);
        }
        else
        {
            FrameworkCore.Event.OnTriggerEven("UpDeteEnemyBuffUI", BuffDic[actor]);
        }

    }
    public void RemoveBuff(Charocter actor, Buff buff)
    {
        if (!BuffDic.ContainsKey(actor))
        {
            return;
        }
        var bufflis = BuffDic[actor];
        BuffDic[actor].Remove(buff);
        buff.RemoveBuff(actor);
        if (BuffDic[actor].Count <= 0)
        {
            BuffDic.Remove(actor);
            actors.Remove(actor);
        }
        if (actor is Player)
        {
            FrameworkCore.Event.OnTriggerEven("UpDetePlayerBuffUI", bufflis);
        }
        else
        {
            FrameworkCore.Event.OnTriggerEven("UpDeteEnemyBuffUI", bufflis);
        }
    }
    public void Clear(Charocter actor)
    {
        if (!BuffDic.ContainsKey(actor))
        {
            return;
        }
        foreach(var buff in BuffDic[actor])
        {
            buff.RemoveBuff(actor);
        }
        var list = BuffDic[actor];
        BuffDic[actor].Clear();
        BuffDic[actor]= null;
        BuffDic.Remove(actor) ;
        actors.Remove(actor );
        if (actor is Player)
        {
            FrameworkCore.Event.OnTriggerEven("UpDetePlayerBuffUI", list);
        }
        else
        {
            FrameworkCore.Event.OnTriggerEven("UpDeteEnemyBuffUI" , list);
        }
    }
    public void Clear()
    {
        if (actors.Count<=0)
        {
            return;
        }
        foreach(var actor in actors)
        {
            BuffDic[actor].Clear();
            BuffDic[actor] = null;
            BuffDic.Remove(actor);
        }
        actors.Clear();
        FrameworkCore.Event.OnTriggerEven("ClearAllBuffUI");
    }
    public void UpDateBuffTime(float time = 1)
    {
        for(int j=0;j<actors.Count;j++)
        {
            Charocter act= actors[j];
            var lis=BuffDic[act]; ;
            if (lis != null)
            {
                for(int i = 0; i < lis.Count; i++)
                {
                    if (lis[i].UpDate(time))
                    {
                        RemoveBuff(act, lis[i]);
                    }
                }
            }
        }
    }
}
