using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsFactory : MonoBehaviour
{
    private static CardsFactory instance;
    public static CardsFactory Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindFirstObjectByType<CardsFactory>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject("CardsFactory");
                instance = gameObject.AddComponent<CardsFactory>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public HandCard CreateHandCard(CardConfig config)
    {
        Buff buff = null;
        BuffTable buffTable = FrameworkCore.Resourse.ResourcesLoad<BuffTable>(ABConfig.Table, "BuffTable");
        if(config.BuffID>=0)
        {
            buff = GameCore.BuffFactory.GetBuff(buffTable.m_BuffList[config.BuffID]);
        }
        //switch (config.BuffID)
        //{
        //    case BuffType.Attack:
        //        buff = new Buff(buffTable.m_BuffList[0]);//TODO：卡牌表里需要有Buff表的引用
        //        break;
        //    case BuffType.Defense:
        //        buff = new Buff(buffTable.m_BuffList[1]);
        //        break;
        //    default:
        //        buff = null;
        //        break;

        //}
        IUse use = null;
        switch (config.type)
        {
            case CardType.Attack:
                use = new AttackUse();
                break;
            case CardType.Defense:
                use = new DefenseUse();
                break;
            default:
                use = null;
                break;

        }
        HandCard card = new HandCard(buff,use,config);
        return card;
    }
}
