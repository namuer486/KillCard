using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : BaseState
{
    public GameInit()
    {
        FrameworkCore core = FrameworkCore.Instance;
        Debug.Log("架构层初始化");
    }
    public override void OnEnter()
    {
        

    }
    public override void OnUpDate()
    {

    }
    public override void OnExit()
    {

    }
}
public class GameMenu : BaseState
{
    public override void OnEnter()
    {

    }
    public override void OnUpDate()
    {

    }
    public override void OnExit() 
    {

    }
}
public class GameCharactor : BaseState
{
    public override void OnEnter()
    {

    }
    public override void OnUpDate()
    {

    }
    public override void OnExit() 
    {

    }
}
public enum GameTempType
{
    init,//初始化
    player,//玩家回合
    enemy//敌人回合
}
public class GamePlay : BaseState
{
    private StateManager<GameTempType> GamePlayManager; //回合控制状态机
    //private StateManager<GameType> GameManager; 
    public GamePlay()
    {
        GamePlayManager = new StateManager<GameTempType>();
        GamePlayManager.AddState(GameTempType.init,new CardInit());
        GamePlayManager.AddState(GameTempType.player,new PlayerNow());
        GamePlayManager.AddState(GameTempType.enemy,new EnemyNow());
        GamePlayManager.ChangeState(GameTempType.init);
    }
    public override void OnEnter()
    {
        FrameworkCore.Event.Add<GameTempType>(this, "ChangeTempState", GamePlayManager.ChangeState);
        FrameworkCore.Event.Add(this, "GamePuaseOver", PuaseOver);
        FrameworkCore.Event.Add(this, "GamePuase", PuaseBegin);
        //挂上暂停Ui
        FrameworkCore.UI.LoadBackGroundUi();
        //TODO:实例化玩家角色
        Debug.Log(GameCore.Player.currentplayer.Config.name + "已被加载");
        //背包卡组加载
        FrameworkCore.Event.OnTriggerEven("InitBag");
        FrameworkCore.Event.OnTriggerEven("CardInit");

    }
    public override void OnUpDate()
    {
        if (GameCore.Instance.Is_Pause)
            return;
        GamePlayManager.currentState.OnUpDate();
    }
    public override void OnExit() 
    {
        GamePlayManager.ChangeState(GameTempType.init);
        GameCore.Instance.Is_Pause = false;
        FrameworkCore.Event.RemoveAll(this);
        FrameworkCore.UI.UnLoadBackGroundUi("PauseUi");
        GameCore.Buff.Clear();
    }
    private void PuaseOver()
    {
        GameCore.Instance.Is_Pause = false;
    }
    private void PuaseBegin()
    {
        GameCore.Instance.Is_Pause = true;
    }
}
public class GameOver : BaseState
{
    public override void OnEnter()
    {

    }
    public override void OnUpDate()
    {

    }
    public override void OnExit() 
    {

    }
}
