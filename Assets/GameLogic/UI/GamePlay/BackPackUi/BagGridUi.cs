using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagGridUi : MonoBehaviour, IPoolable
{
    public CardConfig cardConfig {  get; private set; }
    public TextMeshProUGUI content { get;private set; }
    public TextMeshProUGUI Name { get;private set; }
    public Image image { get;private set; }

    private void Awake()
    {
        content = transform.Find("CardContent").GetComponent<TextMeshProUGUI>();
        Name = transform.Find("CardName").GetComponent<TextMeshProUGUI>();
        image = transform.Find("CardIma").GetComponent<Image>();
    }
    public void UpDateCardUI(CardConfig cardConfig)
    {
        if (cardConfig == null)
        {
            return;
        }
        this.cardConfig = cardConfig;
        content.text = cardConfig.content;
        Name.text = cardConfig.name;
        image.sprite = cardConfig.sprite;
    }
    public void OnGet()
    {
        if (cardConfig == null)
        {
            gameObject.SetActive(true);
            return;
        }
        content.text = cardConfig.content;
        Name.text = cardConfig.name;
        image.sprite = cardConfig.sprite;
        gameObject.SetActive(true);
    }
    public void OnBack()
    {
        cardConfig = null;
        content.text = null;
        Name.text = null;
        image.sprite = null;
        gameObject.SetActive(false);
    }
}
