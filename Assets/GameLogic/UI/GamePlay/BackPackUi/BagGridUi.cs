using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BagGridUi : MonoBehaviour
{
    public CardConfig cardConfig {  get; private set; }
    private void Start()
    {
        if(cardConfig == null)
        {
            Debug.LogError("ø®≈∆UIŒ¥≈‰÷√");
            return;
        }
        TextMeshProUGUI content =  transform.Find("CardContent").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI name =  transform.Find("CardName").GetComponent<TextMeshProUGUI>();
        Image image =transform.Find("CardIma").GetComponent<Image>();
        content.text=cardConfig.content;
        name.text=cardConfig.name;
        image.sprite = cardConfig.sprite;
    }
}
