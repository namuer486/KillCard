using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCardUi : MonoBehaviour,IPoolable,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public HandCard card {  get; internal set; }
    public Vector2 Pos { get; private set; }
    public TextMeshProUGUI text {  get; private set; }
    private void Awake()
    {
        text = transform.Find("CardName").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateUI(HandCard card)
    {
        if (card == null)
        {
            return;
        }
        this.card = card;
        text.text = card.config.name;
    }
    public void OnGet()
    {
        
    }
    public void OnBack()
    {
        card = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Pos = transform.position;
        transform.position = eventData.position;
        transform.Find("CardIma").GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter == null)
        {
            transform.Find("CardIma").GetComponent<Image>().raycastTarget = true;
            transform.position = Pos;
            return;
        }
        if (eventData.pointerEnter.tag == "Enemy")
        {
            EnemyUi enemy = eventData.pointerEnter.transform.GetComponent<EnemyUi>();
            if (enemy != null)
            {
                if (card.config.totype == ToType.enemy)
                {
                    GameCore.HandCard.UseCard(card, enemy.enemy);
                    transform.position = Pos;
                    transform.Find("CardIma").GetComponent<Image>().raycastTarget = true;
                    return;
                }
                
            }
        }else if (eventData.pointerEnter.tag == "Player")
        {
            PlayerUi player = eventData.pointerEnter.transform.GetComponent<PlayerUi>();
            if (player != null)
            {
                if (card.config.totype == ToType.plater)
                {
                    GameCore.HandCard.UseCard(card, player.player);
                    transform.position = Pos;
                    transform.Find("CardIma").GetComponent<Image>().raycastTarget = true;
                    return;
                }

            }
        }
        transform.Find("CardIma").GetComponent<Image>().raycastTarget = true;
        transform.position = Pos;
    }
}
