using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardUi : MonoBehaviour,BasePanel,IPoolable
{
    public CardConfig cardConfig {  get; internal set; }
    public void Open()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        gameObject.SetActive(true);
    }
    public void OnGet()
    {
        FrameworkCore.UI.Push(UILaye.Guide, this);
    }
    public void OnBack()
    {
        FrameworkCore.UI.Pop(UILaye.Guide);
    }
}
