using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "PlayersTable",menuName = "Table/PlayersTable")]
public class PlayersTable : ScriptableObject
{
    public List<PlayerConfig> playerConfigs = new List<PlayerConfig>();

}
[System.Serializable]
public class PlayerConfig
{
    public int id;
    public string name;
    public int HP;
    public int KardNumber;//³õÊ¼¿¨ÅÆÊý
    public Sprite sprite = null;
}
