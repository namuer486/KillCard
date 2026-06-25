using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftProp : MonoBehaviour
{
    public Button CloseButton;


    public Pool<GiftUI> pool {  get; private set; }
    public List<GiftUI> uIs { get; private set; }
    public GiftUI uI {  get; private set; }
    public int Num { get; private set; } = 3;
    public Vector2 posCard { get; private set; } = new Vector2(-150, 50);
    public float Length { get; private set; } = -150;
    public void Awake()
    {
        uIs = new List<GiftUI>();
        uI = FrameworkCore.Resourse.ResourcesLoad<GiftUI>(ABConfig.Card, "Prefab/GiftCardPre");
        pool = new Pool<GiftUI>(uI, 5, transform);
        CloseButton.onClick.AddListener(Close);
    }

    public void OnEnable()
    {
        gameObject.SetActive(true);
        Vector2 pos = posCard;
        for (int i = 0; i < Num; i++)
        {
            GiftUI uI = pool.Get();
            uI.Init(pos);
            uIs.Add(uI);
            FrameworkCore.UI.Push(UILaye.Popup, uI);
            pos += Vector2.left * Length;
        }
        transform.SetAsLastSibling();

    }

    public void Close()
    {
        FrameworkCore.Senes.UnLoadTempScene(TempScene.AttackWin);
    }
}
