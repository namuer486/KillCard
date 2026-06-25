using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBuff
{
    public void AddBuff(Charocter actor,float time = 1);
    public void RemoveBuff(Charocter actor);
    public bool UpDate(float deltatime);
}

public class Buff : IBuff
{
    public BuffConfig buff { get; protected set; }//TODO：外部注入
    public Buff(BuffConfig buff)
    {
        this.buff = buff;
    }
    public void AddBuff(Charocter actor, float time = 1)
    {
        buff.time = time;
        //事件广播效果
        Debug.Log(buff.buffType+"Buff Add");
    }
    public void RemoveBuff(Charocter actor)
    {
        Debug.Log(buff.buffType + "Buff Remove");
    }
    public bool UpDate(float deltatime)
    {
        buff.timer += deltatime;
        if (buff.timer > buff.time)
        {
            buff.timer = 0;
            return true;
        }
        return false;
    }
}
public class AttackBuff:IBuff
{
    public BuffConfig buff { get; protected set; }
    public AttackBuff()
    {
        buff.name = "AttackUp";
    }
    public void AddBuff(Charocter actor, float time = 1)
    {
        buff.time = time;
        //事件广播效果
        Debug.Log("攻击Buff Add");
    }
    public void RemoveBuff(Charocter actor)
    {
        Debug.Log("攻击Buff Remove");
    }
    public bool UpDate(float deltatime)
    {
        buff.timer += deltatime;
        if (buff.timer > buff.time) 
        {
            buff.timer = 0;
            return true;
        }
        return false;
    }
}
public class DefenseBuff :IBuff
{
    public BuffConfig buff { get; protected set; }
    public DefenseBuff()
    {
        buff.name = "DefenseUp";
    }
    public void AddBuff(Charocter actor, float time = 1)
    {
        buff.time = time;
        Debug.Log("防御Buff Add");
    }
    public void RemoveBuff(Charocter actor)
    {
        Debug.Log("防御Buff Remove");
    }
    public bool UpDate(float deltatime)
    {
        buff.timer += deltatime;
        if (buff.timer > buff.time)
        {
            buff.timer = 0;
            return true;
        }
        return false;
    }
}
