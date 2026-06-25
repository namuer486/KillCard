using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardManager//战斗管理器
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
    public Queue<HandCard> Pushcards = new Queue<HandCard>();//抽牌堆
    public List<HandCard> handCards = new List<HandCard>();//手牌堆
    public Queue<HandCard> Popcards = new Queue<HandCard>();//弃牌堆
    public int Num { get; private set; } = 5;//手牌数量
    public HandCardManager()
    {
        FrameworkCore.Event.Add<int>(this, "SetCurrentNum", SetNum);
        FrameworkCore.Event.Add(this, "CardInit", GetCardToPush);
        FrameworkCore.Event.Add(this, "CardPush", GetCardToHand);
        FrameworkCore.Event.Add(this, "CardPop", GetCardToPop);
    }
    private void SetNum(int value)
    {
        Num = value;
    }
    public void GetCardToPush()//战斗开始从背包获取卡牌去抽牌堆
    {
        HandCard card = GameCore.BackPack.Get();
        while(card != null)
        {
            Pushcards.Enqueue(card);
            card = GameCore.BackPack.Get();
        }
        //FrameworkCore.Event.OnTriggerEven("ChangeTempState", GameTempType.player);
    }
    public void GetCardToHand()//拿去卡牌进入手牌
    {
        for(int i = 0; i < Num; i++)
        {
            HandCard card = Pushcards.Dequeue();
            if (card==null)
            {
                ResetPush();
                card = Pushcards.Dequeue();//不能让卡组内为空
            }
            handCards.Add(card);
        }
        FrameworkCore.Event.OnTriggerEven("UpDateHandCardUI", handCards);
    }
    public void ResetPush()//重新洗牌
    {
        //Pushcards.Clear();
        HandCard card = Popcards.Count <= 0 ? null : Popcards.Dequeue();
        while(card != null)
        {
            Pushcards.Enqueue(card);
            card = Popcards.Count<=0 ? null : Popcards.Dequeue();
        }
        
    }
    public void UseCard(HandCard card,Charocter actor)//手牌消耗
    {
        HandCard temp = card;
        card.Use?.Use(card.value, actor);
        if(card.buff != null)
        {
            GameCore.Buff.AddBuff(actor, card.buff);
        }
        handCards.Remove(card);
        Popcards.Enqueue(temp);
        FrameworkCore.Event.OnTriggerEven("UpDateHandCardUI", handCards);
    }
    public void GetCardToPop()//回合结束
    {
        foreach(var card in handCards)
        {
            if (card != null)
            {
                Popcards.Enqueue(card);
            }
        }
        handCards.Clear();
        if(Pushcards.Count <= 0)
        {
            ResetPush();
        }
        FrameworkCore.Event.OnTriggerEven("ClearAllHandCard");
    }
    public HandCard RandowCardGet()
    {
        CardsTable table = FrameworkCore.Resourse.ResourcesLoad<CardsTable>(ABConfig.Table, "CardsTable");
        CardConfig config = table.RandowGet();
        HandCard card = GameCore.CardsFactory.CreateHandCard(config);
        return card;
    }
}
