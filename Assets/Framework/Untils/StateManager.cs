using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState//基类节点
{
    public abstract void OnEnter();
    public abstract void OnUpDate();
    public abstract void OnExit();
}
public class StateManager<StateID>//根据枚举类型创建
{
    public Dictionary<StateID, BaseState> pairs {  get; private set; }
    public BaseState currentState { get; private set; }
    public StateManager()
    {
        pairs=new Dictionary<StateID, BaseState>();
        currentState = null;
    }
    public void AddState(StateID id, BaseState state)
    {
        if (pairs.ContainsKey(id))
            return;
        pairs[id] = state;
    }
    public void ChangeState(StateID id)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState=pairs[id];
        currentState.OnEnter();
        
    }
    public void OnUpDate()
    {
        if (currentState != null)
            currentState.OnUpDate();
    }
}
