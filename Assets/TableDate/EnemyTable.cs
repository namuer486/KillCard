using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTable",menuName = "Table/EnemyTable")]
public class EnemyTable : ScriptableObject
{
    public List<EnemyConfig> enemies = new List<EnemyConfig>();
}
[System.Serializable]
public class EnemyConfig
{
    public int id;
    public string name;
    public int HP;
    public EnemyType type;
    public Sprite sprite = null;
}
public enum EnemyType
{
    soldier,
    boss
}
