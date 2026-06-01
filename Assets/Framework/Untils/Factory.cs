using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    private static Factory instance = null;//TODO:通用工厂(废弃)
    public static Factory Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            instance = new Factory();
            return instance;
        }
    }

}
