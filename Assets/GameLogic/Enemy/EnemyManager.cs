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
        EnemyTable configs = FrameworkCore.Resourse.ResourcesLoad<EnemyTable>(ABConfig.Table,"EnemyTable");
        BuffTable buffconfigs = FrameworkCore.Resourse.ResourcesLoad<BuffTable>(ABConfig.Table, "BuffTable");
        List<Buff> buffs = new List<Buff>();//出招表
        for (int i = 0;i<configs.enemies.Count;i++)
        {
            Buff buff=GameCore.BuffFactory.GetBuff(buffconfigs.m_BuffList[0]);
            buffs.Add(buff);
            Enemy enemy = new Enemy(configs.enemies[i], buffs,this);
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
        currentenemy.Reset();
    }
    public void EnemyDie(Enemy enemy)
    {
        enemies.Remove(enemy);
        currentenemy=null;
        //返回地图
        //弹出奖励窗口
        FrameworkCore.Event.OnTriggerEven("AttackWin");

    }
}
