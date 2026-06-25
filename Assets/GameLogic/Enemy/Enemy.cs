using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType
{
    wait,
    attackplayer,
    dead
}
public class Enemy: Charocter//DOTO:敌人动态属性配置，意图系统搭建
{
    public EnemyManager enemyManager = null;
    public EnemyConfig Config {  get; internal set; }
    public List<Buff> Buffs { get; internal set; }
    public float HP {  get; internal set; }
    public float DefrenceHP { get; internal set; } = 0;
    public StateManager<EnemyStateType> manager { get; internal set; }
    public Enemy(EnemyConfig config, List<Buff> Buffs,EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        this.Buffs = Buffs;
        Config = config;
        HP = config.HP;
        manager = new StateManager<EnemyStateType>();
        manager.AddState(EnemyStateType.wait,new EnemyWaitState(this, manager));
        manager.AddState(EnemyStateType.attackplayer,new EnemyAttackPlayerState(this, manager));
        manager.AddState(EnemyStateType.dead,new EnemyDeadState(this, manager));
        manager.ChangeState(EnemyStateType.wait);
    }
    public void Attack()
    {
        GameCore.Player.currentplayer.Hurt(10);
    }
    public void AddBuff()//需要配置专属buff池
    {
        Buff buff = Buffs[0];
        if (buff == null)
            return;
        GameCore.Buff.AddBuff(GameCore.Player.currentplayer, buff);
    }
    //public void AddDeBuff(Buff buff)
    //{
    //    GameCore.Buff.AddBuff(GameCore.Player.currentplayer, buff);
    //}
    public void Defrence()
    {
        DefrenceHP += 10;
    }
    public override void Hurt(float value)
    {
        DefrenceHP -= value;
        if (DefrenceHP <= 0)
        {
            HP += DefrenceHP;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
            FrameworkCore.Event.OnTriggerEven("UpDateEnemyUI");
        }
    }
    public override void Die()
    {
        enemyManager.EnemyDie(this);
    }
    public override void HpBack(float value)
    {
        HP += value;
        if(HP>=Config.HP)
        {
            HP = Config.HP;
        }
    }
    public void Reset()
    {
        HP = Config.HP;
        DefrenceHP = 0;
        manager.ChangeState(EnemyStateType.wait);
    }
}
