using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUi : MonoBehaviour
{
    public CardUi CardUI {  get; private set; }
    public int CharacterNum { get; private set; } = 3;
    public Vector2 PosKard { get; private set; } = new Vector2(-300, 0);
    public float LengthKard { get; private set; } = 200;
    // Start is called before the first frame update
    void Start()
    {
        InitCardCharactor();
    }
    public void InitCardCharactor()
    {
        CardUI = FrameworkCore.Resourse.ResourcesLoad<CardUi>(ABConfig.Card, "Card");
        for (int i = 0; i < CharacterNum; i++)//TODO：后续创建转移到工厂
        {
            CardUi kard = Instantiate<CardUi>(CardUI);
            kard.transform.SetParent(transform, false);
            kard.GetComponent<RectTransform>().localPosition = PosKard;
            CardUi kardui = kard.GetComponent<CardUi>();
            PlayersTable table = FrameworkCore.Resourse.ResourcesLoad<PlayersTable>(ABConfig.Table, "PlayersTable");
            kardui.config = table.playerConfigs[i];
            FrameworkCore.UI.Push(UILaye.Popup, kardui);
            PosKard = PosKard + Vector2.right * LengthKard;
        }
    }

    private void OnDestroy()
    {
        FrameworkCore.UI.ClearPopup();
    }
}
