using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCardPool//½±Àø¿¨³Ø
{
    private static GiftCardPool instance;
    public static GiftCardPool Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            instance = new GiftCardPool();
            return instance;
        }
    }
}
