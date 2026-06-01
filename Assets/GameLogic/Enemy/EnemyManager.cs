using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new EnemyManager();
            return instance;
        }
    }
    public List<Enemy> enemies { get; private set; }
    public Enemy currentenemy { get; private set; }//Tip提供查询，管理器职能，不需要分离
    public EnemyManager()
    {
        enemies = new List<Enemy>();
        EnemyTable configs = FrameworkCore.Resourse.ResourcesLoad<EnemyTable>("EnemyTable");
        foreach (EnemyConfig cfg in configs.enemies)
        {
            Enemy enemy = new Enemy(cfg);
            enemies.Add(enemy);
        }
        FrameworkCore.Event.Add<int>(this, "LoadCurrentEnemy", LoadCurrentEnemy);
    }
    public void LoadCurrentEnemy(int id)
    {
        if (id < 0||id>= enemies.Count)
        {
            Debug.LogError("怪物索引超出界限");
            return;
        }
        currentenemy = enemies[id];
    }
}
