using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInit : BaseState//TODO：完成UI设计和放置
{
    public override void OnEnter()
    {

    }
    public override void OnUpDate()
    {

    }
    public override void OnExit()
    {
        Debug.Log("初始化结束");
    }
}
public class PlayerNow : BaseState
{
    public override void OnEnter()
    {
        GameCore.HandCard.GetCardToHand();
        GameCore.Buff.UpDateBuffTime();
        Debug.Log("拿牌");
    }
    public override void OnUpDate()
    {

    }
    public override void OnExit()
    {
        FrameworkCore.Event.OnTriggerEven("CardPop");
        Debug.Log("洗牌");
    }
}
public class EnemyNow : BaseState
{
    public override void OnEnter()
    {
        Debug.Log("敌人回合");
        GameCore.Enemy.currentenemy.manager.ChangeState(EnemyStateType.attackplayer);
    }
    public override void OnUpDate()
    {
        //敌人状态机运行        GameCore.Enemy.currentenemy
        GameCore.Enemy.currentenemy.manager.OnUpDate();
        
        //FrameworkCore.Event.OnTriggerEven("ChangeTempState",GameTempType.player);
    }
    public override void OnExit()
    {
        Debug.Log("敌人回合结束");
        
    }
}
