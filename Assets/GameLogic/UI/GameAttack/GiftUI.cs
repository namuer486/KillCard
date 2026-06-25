using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftUI : MonoBehaviour,BasePanel, IPoolable,IPointerClickHandler
{
    public HandCard card {  get; private set; }
    public TextMeshProUGUI Name { get; private set; }
    public TextMeshProUGUI Content { get; private set; }
    public void Init(Vector2 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
        Name = transform.Find("CardName").GetComponent<TextMeshProUGUI>();
        Content = transform.Find("CardContent").GetComponent<TextMeshProUGUI>();
        if (card != null)
        {
            Name.text = card.config.name;
            Content.text = card.config.content;
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnBack()
    {
        Close();
        card = null;
    }

    public void OnGet()
    {
        //ø®≥ÿ≥È»°
        Open();
        card = GameCore.HandCard.RandowCardGet();
    }

    public void Open()
    {

        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(card != null)
        {
            GameCore.BackPack.Add(card);
            FrameworkCore.Senes.UnLoadTempScene(TempScene.AttackWin);
        }
    }
}
