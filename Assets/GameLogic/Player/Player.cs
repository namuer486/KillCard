using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player: Charocter
{

    public PlayerConfig Config { get; private set; }
    public float HP {  get; private set; }
    public PlayerManager Manager { get; private set; }

    public Player(PlayerConfig config,PlayerManager manager)
    {
        this.Config = config;
        Manager = manager;
    }
    public void Reset()
    {
        HP = Config.HP;
        FrameworkCore.Event.OnTriggerEven("UpDateUI");
    }
    public override void Hurt(float value)
    {
        HP -= value;
        if (HP <= 0)
        {
            HP = 0;
            Die();
            return;
        }
        FrameworkCore.Event.OnTriggerEven("UpDateUI");
        
    }
    public override void HpBack(float value)
    {
        HP += value;
        if (HP >= Config.HP)
        {
            HP = Config.HP;
        }
        FrameworkCore.Event.OnTriggerEven("UpDateUI");
    }
    public override void Die()
    {
        //gameover
        FrameworkCore.Senes.ChangeScene(GameType.over);
        Manager.CurrentPlayerDie();
    }
}
