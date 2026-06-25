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
    public int Index = 0;//抽牌顺序
    public int Count = 5;
    public BackPackManager()
    {
        //初始化十张卡牌在背包里
        FrameworkCore.Event.Add(this, "InitBag", InitBag);
    }
    public void InitBag()
    {
        CardsTable cards = FrameworkCore.Resourse.ResourcesLoad<CardsTable>(ABConfig.Table,"CardsTable");
        for (int i = 0; i < Count; i++)
        {
            CardConfig config = cards.kards[i];
            HandCard card = GameCore.CardsFactory.CreateHandCard(config);
            Add(i, card);
        }
    }
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
        //if(bag.handCards.Length>=bag.GridNum)
        //{
        //    Debug.LogError("背包已满");
        //    return false;
        //}
        //TODO:背包自动扩容
        if (bag.handCards[idx] != null)
        {
            return false;
        }
        bag.handCards[idx] = card;
        FrameworkCore.Event.OnTriggerEven("UpDateBackUI", bag);
        return true;
    }
    public bool Add(HandCard card)
    {
        if(card == null)
        {
            Debug.LogError("背包添加物品为空");
            return false;
        }
        //TODO:背包自动扩容
        int idx = bag.SourchNullGrid();
        if (bag.handCards[idx] != null)
        {
            return false;
        }
        bag.handCards[idx] = card;
        FrameworkCore.Event.OnTriggerEven("UpDateBackUI", bag);
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
        FrameworkCore.Event.OnTriggerEven("UpDateBackUI", bag);
    }
    public HandCard Get()
    {
        Index = Index % bag.GridNum;
        while (bag.handCards[Index] == null)
        {
            Index++;
            if (Index >= bag.GridNum)
                return null;
        }
        return bag.handCards[Index++];
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
        FrameworkCore.Event.OnTriggerEven("UpDateBackUI", bag);
    }
}
