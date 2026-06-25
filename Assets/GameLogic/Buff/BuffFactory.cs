using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFactory
{
    private static BuffFactory instance;
    public static BuffFactory Instance
    {
        get
        {
            if(instance!=null) return instance;
            instance = new BuffFactory();
            return instance;
        }
    }
    public Buff GetBuff(BuffConfig config)
    {
        if(config == null)
            return null;
        Buff buff=new Buff(config);
        return buff;
    }
}
