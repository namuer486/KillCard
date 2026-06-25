using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTempType
{
    none,
    attack,
    defrence,
    buff,
}
public class EnemyWaitState : BaseState
{
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyWaitState(Enemy enemy, StateManager<EnemyStateType> stateManager)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
    }
    public override void OnEnter()
    {
        //FrameworkCore.Event.OnTriggerEven("ChangeTempState", GameTempType.player);
        //Debug.Log("怪兽回合结束");
    }

    public override void OnExit()
    {

    }

    public override void OnUpDate()
    {
        if (enemy.HP < 0)
        {
            stateManager.ChangeState(EnemyStateType.dead);
        }
    }
}
public class EnemyAttackPlayerState : BaseState
{
    public EnemyTempType next {  get; private set; }
    public Enemy enemy {  get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }
    public StateManager<EnemyTempType> stateTempManager { get; private set; }

    public EnemyAttackPlayerState(Enemy enemy, StateManager<EnemyStateType> stateManager)
    {
        next= EnemyTempType.attack;
        this.enemy = enemy;
        this.stateManager = stateManager;
        stateTempManager = new StateManager<EnemyTempType>();
        stateTempManager.AddState(EnemyTempType.none, new EnemyTempNoneState(enemy, stateManager, 0));
        stateTempManager.AddState(EnemyTempType.attack,new EnemyTempAttackState(enemy, stateManager, 40));
        stateTempManager.AddState(EnemyTempType.defrence,new EnemyTempDefrenceState(enemy, stateManager, 40));
        stateTempManager.AddState(EnemyTempType.buff, new EnemyTempBuffState(enemy, stateManager, 20));
        stateTempManager.ChangeState(EnemyTempType.none);
    }
    public override void OnEnter()
    {

        stateTempManager.ChangeState(next);
    }

    public override void OnExit()
    {
        //预告下次意图
        int value = Random.Range(0, 100);
        if (value < 40)
        {
            next = EnemyTempType.attack;
        }else if(value < 80)
        {
            next = EnemyTempType.defrence;
        }
        else
        {
            next = EnemyTempType.buff;
        }
        FrameworkCore.Event.OnTriggerEven("EnemyWant", next);
        stateTempManager.ChangeState(EnemyTempType.none);
        Debug.Log("下次意图" + next);
        FrameworkCore.Event.OnTriggerEven("ChangeTempState", GameTempType.player);
    }

    public override void OnUpDate()
    {
        if (enemy.HP <= 0)
        {
            stateManager.ChangeState(EnemyStateType.dead);
            
        }
        stateTempManager.OnUpDate();
    }
}
public class EnemyDeadState : BaseState
{
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyDeadState(Enemy enemy, StateManager<EnemyStateType> stateManager)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
    }
    public override void OnEnter()
    {
        enemy.Die();
        //返回地图，给予奖励
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpDate()
    {

    }
}
