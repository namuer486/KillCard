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
    normal,
    attack
}
public class GamePlay : BaseState
{
    private StateManager<GameTempType> GamePlayManager; 
    private StateManager<GameType> GameManager; 
    public GamePlay()
    {
        GamePlayManager = new StateManager<GameTempType>();
        GamePlayManager.AddState(GameTempType.normal,new GameNormal());
        GamePlayManager.AddState(GameTempType.attack,new GameAttack());
        GamePlayManager.ChangeState(GameTempType.normal);
    }
    public override void OnEnter()
    {
        FrameworkCore.Event.Add<GameTempType>(this, "ChangeTempState", GamePlayManager.ChangeState);
        FrameworkCore.Event.Add(this, "GamePuaseOver", PuaseOver);
        FrameworkCore.Event.Add(this, "GamePuase", PuaseBegin);
        //挂上暂停Ui
        FrameworkCore.UI.LoadBackGroundUi();
        //TODO:实例化玩家角色
        Debug.Log(GameCore.Player.currentplayer.config.name + "已被加载");

    }
    public override void OnUpDate()
    {
        if (GameCore.Instance.Is_Pause)
            return;
        GamePlayManager.currentState.OnUpDate();
    }
    public override void OnExit() 
    {
        GamePlayManager.ChangeState(GameTempType.normal);
        GameCore.Instance.Is_Pause = false;
        FrameworkCore.Event.RemoveAll(this);
        FrameworkCore.UI.UnLoadBackGroundUi("PauseUi");
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
