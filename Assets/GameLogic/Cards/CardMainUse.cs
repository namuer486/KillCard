using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUse
{
    public void Use(float value, Charocter actor);
}

public class AttackUse : IUse
{
    public void Use(float value, Charocter actor)
    {
        //事件广播
        actor.Hurt(value);
        Debug.Log("攻击" + value);
    }
}
public class DefenseUse : IUse
{
    public void Use(float value, Charocter actor)
    {
        //事件广播
        actor.Hurt(value);
        Debug.Log("防御"  + value);
    }
}
