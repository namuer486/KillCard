using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTempNoneState : BaseState
{
    public int weight { get; set; }
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyTempNoneState(Enemy enemy, StateManager<EnemyStateType> stateManager, int weight)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
        this.weight = weight;
    }
    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnUpDate()
    {

    }
}
public class EnemyTempAttackState : BaseState
{
    public int weight { get; set; }
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyTempAttackState(Enemy enemy, StateManager<EnemyStateType> stateManager, int weight)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
        this.weight = weight;
    }
    public override void OnEnter()
    {
        enemy.Attack();
    }

    public override void OnExit()
    {

    }

    public override void OnUpDate()
    {
        stateManager.ChangeState(EnemyStateType.wait);
    }
}
public class EnemyTempBuffState : BaseState
{
    public int weight { get; set; }
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyTempBuffState(Enemy enemy, StateManager<EnemyStateType> stateManager, int weight)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
        this.weight = weight;
    }
    public override void OnEnter()
    {
        enemy.AddBuff();
    }

    public override void OnExit()
    {

    }

    public override void OnUpDate()
    {
        stateManager.ChangeState(EnemyStateType.wait);
    }
}
public class EnemyTempDefrenceState : BaseState
{
    public int weight { get; set; }
    public Enemy enemy { get; private set; }
    public StateManager<EnemyStateType> stateManager { get; private set; }

    public EnemyTempDefrenceState(Enemy enemy, StateManager<EnemyStateType> stateManager, int weight)
    {
        this.enemy = enemy;
        this.stateManager = stateManager;
        this.weight = weight;
    }
    public override void OnEnter()
    {
        enemy.Defrence();
    }

    public override void OnExit()
    {

    }

    public override void OnUpDate()
    {
        stateManager.ChangeState(EnemyStateType.wait);
    }
}
