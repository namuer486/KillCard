using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUi : MonoBehaviour,BasePanel
{
    public Button CardUI {  get; private set; }
    public int CharacterNum { get; private set; } = 3;
    public Vector2 PosKard { get; private set; } = new Vector2(-300, 0);
    public float LengthKard { get; private set; } = 200;
    // Start is called before the first frame update
    void OnEnable()
    {
        FrameworkCore.UI.Push(UILaye.Main, this);
        CardUI = FrameworkCore.Resourse.ResourcesLoad<Button>("Prefab/Card");
        for (int i = 0; i < CharacterNum; i++)//TODO：后续创建转移到工厂
        {
            Button kard = Instantiate<Button>(CardUI);
            kard.transform.SetParent(transform, false);
            kard.GetComponent<RectTransform>().localPosition = PosKard;
            CardUi kardui = kard.GetComponent<CardUi>();
            PlayersTable table = FrameworkCore.Resourse.ResourcesLoad<PlayersTable>("PlayersTable");
            kardui.config = table.playerConfigs[i];
            FrameworkCore.UI.Push(UILaye.Popup, kardui);
            PosKard = PosKard + Vector2.right * LengthKard;
        }
    }

    private void OnDestroy()
    {
        FrameworkCore.UI.Pop(UILaye.Main);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
