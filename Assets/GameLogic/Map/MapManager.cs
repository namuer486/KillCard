using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            instance = new MapManager();
            return instance;
        }
    }
    public MapDate date { get; private set; }
    public int CurrentIdx { get; private set; } = 0;
    public MapManager()
    {
        date = new MapDate();
        FrameworkCore.Event.Add(this, "AttackWin", OpenBeat);
    }
    public MapDate GetMapDate(int Num = 10)
    {
        date.RandowCreateBeats(Num);
        return date;
    }
    public void OpenBeat()
    {
        CurrentIdx++;
        date.OpenNextBeat(CurrentIdx);
    }
}
