using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardManager//TODO:完成卡牌分发功能（与背包交互）,完成背包UI，
{
    private static HandCardManager instance = null;
    public static HandCardManager Instance
    {
        get
        {
            if(instance != null)
                return instance;
            instance= new HandCardManager();
            return instance;
        }
    }
    public Vector2 PosCard { get; private set; } = new Vector2(-250, -70);
    public int Num { get; private set; } = 0;//分发数量
    public HandCardManager()
    {
        FrameworkCore.Event.Add<int>(this, "SetCurrentNum", SetNum);
    }
    private void SetNum(int value)
    {
        Num = value;
    }
    public void GetCardByBag()
    {

    }
}
