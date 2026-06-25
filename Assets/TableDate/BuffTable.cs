using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffTable",menuName = "Table/BuffTable")]
public class BuffTable : ScriptableObject
{
    public List<BuffConfig> m_BuffList=new List<BuffConfig>();
}
public enum BuffType
{
    None,
    Attack,
    Defense
}
[System.Serializable]
public class BuffConfig//Buff需要单独建表外部导入
{
    public float timer { get; internal set; }//计时器
    public int ID;
    public string name;
    public float time;//持续时间
    public int count;//层数
    public BuffType buffType;
}
