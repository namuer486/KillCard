using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None,//纯Buff
    Attack,//攻击
    Hp,//恢复
    Defense//护盾
}
public enum CardBuff
{
    None,//无buff
    Attack,//攻击力buff
    Defense//防御力buff
}
[CreateAssetMenu(fileName = "CardsTable", menuName = "Table/CardsTable")]
public class CardsTable : ScriptableObject
{
    public List<CardConfig> kards = new List<CardConfig>();
}
[System.Serializable]
public class CardConfig
{
    public int id;//序号
    public string name;//名称
    public string content;//内容
    public CardType type;//卡牌类型
    public CardBuff buff;//Buff类型
    public float number;//卡牌数值
    public float buffnumber;//Buff数值
    public int livenumber;//存在回合数
    public Sprite sprite = null;//图片
}

