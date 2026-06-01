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
        IBuff buff;
        switch(config.buff)
        {
            case CardBuff.Attack:
                buff = new AttackBuff();
                break;
            case CardBuff.Defense:
                buff = new DefenseBuff();
                break;
            default:
                buff = null;
                break;

        }
        HandCard card = new HandCard(buff,config);
        return card;
    }
}
