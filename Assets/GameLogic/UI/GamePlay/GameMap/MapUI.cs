using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public MapDate date {  get; private set; }
    public BeatUI Beat {  get; private set; }
    public Vector2 PosBeat { get; private set; } = new Vector2(-800, 0);
    public float Length { get; private set; } = -100;
    public Pool<BeatUI> pool { get; private set; }
    public List<BeatUI> uis {  get; private set; }
    void Start()
    {
        date = MapManager.Instance.GetMapDate();
        Beat = FrameworkCore.Resourse.ResourcesLoad<BeatUI>(ABConfig.Map, "Beat");
        pool = new Pool<BeatUI>(Beat, 10, transform);
        uis=new List<BeatUI>();
        InitMap();
    }
    private void InitMap()
    {
        Vector2 pos = PosBeat;
        foreach(var beat in date.Beats)
        {
            BeatUI ui = pool.Get();
            ui.beat = beat;
            ui.GetComponent<RectTransform>().localPosition = pos;
            pos += Vector2.left * Length;
            uis.Add(ui);
        }
    }
    // Update is called once per frame
    void OnDisable()
    {
        foreach(var ui in uis)
        {
            pool.Back(ui);
        }
        pool.Clear();
    }
}
