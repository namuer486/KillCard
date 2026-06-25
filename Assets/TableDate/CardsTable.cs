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
public enum ToType
{
    plater,
    enemy
}
[CreateAssetMenu(fileName = "CardsTable", menuName = "Table/CardsTable")]
public class CardsTable : ScriptableObject
{
    public List<CardConfig> kards = new List<CardConfig>();//普通卡池
    public int weight { get; private set; } = 80;

    public List<CardConfig> lesskards = new List<CardConfig>();//稀有卡池
    public int lessweight { get; private set; } = 20;
    public CardConfig RandowGet()
    {
        int wei = Random.Range(0, weight+lessweight);
        if (wei < weight)
        {
            int idx=Random.Range(0, kards.Count);
            return kards[idx];
        }else if(wei <weight+lessweight)
        {
            int idx = Random.Range(0, kards.Count);//TODO:添加稀有卡池
            return kards[idx];
        }
        return null;
    }
    public CardConfig Get(int idx)
    {
        if (idx <= 0 || idx > kards.Count)
        {
            return null;
        }
        return kards[idx];
    } 
}
[System.Serializable]
public class CardConfig
{
    public int id;//序号
    public string name;//名称
    public string content;//内容
    public CardType type;//卡牌类型
    public int BuffID;//存储的Buff
    public ToType totype;//作用对象
    public float number;//卡牌数值
    public Sprite sprite = null;//图片
    public int weight;
}

