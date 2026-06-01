using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBuff
{
    public void AddBuff();
    public void RemoveBuff();
}
public class AttackBuff : IBuff
{
    public void AddBuff()
    {
        //事件广播效果
    }
    public void RemoveBuff()
    {

    }
}
public class DefenseBuff : IBuff
{
    public void AddBuff()
    {

    }
    public void RemoveBuff()
    {

    }
}
