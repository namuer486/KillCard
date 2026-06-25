using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MapDate
{
    public List<Beat> Beats { get; private set; }
    public List<int> Indexs { get; private set; }
    public EnemyTable table {  get; private set; }
    public MapDate()
    {
        table = FrameworkCore.Resourse.ResourcesLoad<EnemyTable>(ABConfig.Table, "EnemyTable");
        Beats = new List<Beat>();
        Indexs=new List<int>();
    }
    public void RandowCreateBeats(int Num)
    {
        Beats.Clear();
        RandowSort(table.enemies.Count);
        for(int i = 0; i < Num; i++)
        {
            Beat beat = new Beat(Indexs[i % Indexs.Count]);
            Beats.Add(beat);
        }
        Beats[0].isOpen = true;
    }
    public void RandowSort(int Num)
    {
        for (int i = 0; i < Num; i++)
        {
            Indexs.Add(i);
        }
        for (int i = Num - 1; i >= 0; i--)
        {
            int idx = Random.Range(0, i);
            int value = Indexs[i];
            Indexs[i] = Indexs[idx];
            Indexs[idx] = value;
        }
    }
    public void OpenNextBeat(int idx)
    {
        Beats[idx].isOpen = true;
        if (idx > 0)
        {
            Beats[idx - 1].isOpen = false;
        }
    }
}
