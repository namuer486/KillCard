using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = new PlayerManager();
            return instance;
        }
    }
    public List<Player> players;
    public Player currentplayer {  get; private set; }//Tip提供查询，管理器职能，不需要分离

    public PlayerManager()
    {
        players = new List<Player>();
        PlayersTable table = FrameworkCore.Resourse.ResourcesLoad<PlayersTable>("PlayersTable");
        foreach (PlayerConfig p in table.playerConfigs)
        {
            Player player = new Player(p);
            players.Add(player);
        }
    }
    public void LoadPlayer(int id)
    {
        if (id < 0 || id >= players.Count)
        {
            Debug.LogError("玩家id不存在");
            return;
        }
        currentplayer = players[id];
    }
}
