using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public EnemyConfig Config {  get; internal set; }
    //TODO:Buff
    public Enemy(EnemyConfig config)
    {
        Config = config;
    }
    public void Hurt()
    {

    }
    public void AddBuff()
    {

    }
    public void RemoveBuff()
    {

    }
}
