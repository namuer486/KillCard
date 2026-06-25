using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffUI : MonoBehaviour, IPoolable
{
    public Buff buff {  get; private set; }
    public TextMeshProUGUI text {  get; private set; }
    private void Awake()
    {
        text = transform.Find("Content").GetComponent<TextMeshProUGUI>();
    }
    public void UpDateUI(Buff buff)
    {
        this.buff = buff;
        text.text = buff.buff.name + " " + buff.buff.time;
    }

    public void OnGet()
    {
        gameObject.SetActive(true);
    }

    public void OnBack()
    {
        gameObject.SetActive(false);
        buff = null;
    }
}
