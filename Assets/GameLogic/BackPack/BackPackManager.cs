using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackManager//TODO:完成卡组储存
{
    private static BackPackManager instance = null;
    public static BackPackManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = new BackPackManager();
            return instance;
        }
    }
    public BagDate bag = new BagDate();
    public bool Add(int idx,HandCard card)
    {
        if(idx < 0)
        {
            Debug.LogError("背包数组越界");
            return false;
        }
        if(card == null)
        {
            Debug.LogError("背包添加物品为空");
            return false;
        }
        if(bag.handCards.Length>=bag.GridNum)
        {
            Debug.LogError("背包已满");
            return false;
        }
        if (bag.handCards[idx] != null)
        {
            return false;
        }
        bag.handCards[idx] = card;
        FrameworkCore.Event.OnTriggerEven("BackPackUiUpDate", bag);
        return true;
    }
    public void Remove(int idx)
    {
        if (idx < 0)
        {
            Debug.LogError("背包数组越界");
            return;
        }
        bag.handCards[idx] = null;
        FrameworkCore.Event.OnTriggerEven("BackPackUiUpDate", bag);
    }
    public void ExChange(int idx1,int idx2)
    {
        if (idx1 < 0 || idx2 < 0)
        {
            Debug.LogError("背包数组越界");
            return;
        }
        if (bag.handCards[idx2] == null)
        {
            Add(idx1, bag.handCards[idx1]);
            bag.handCards[idx1] = null;
            return;
        }
        HandCard temp= bag.handCards[idx1];
        bag.handCards[idx1] = bag.handCards[idx2];
        bag.handCards[idx2] = temp;
        FrameworkCore.Event.OnTriggerEven("BackPackUiUpDate", bag);
    }
}
