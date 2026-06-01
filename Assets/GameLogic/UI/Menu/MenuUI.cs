using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour,BasePanel
{
    public Button StartUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartUI.onClick.AddListener(ChangeToStart);
        FrameworkCore.UI.Push(UILaye.Main, this);
    }
    private void ChangeToStart()
    {
        FrameworkCore.Senes.ChangeScene(GameType.charactor);
    }
    private void OnDestroy()
    {
        StartUI.onClick.RemoveAllListeners();
        FrameworkCore.UI.Pop(UILaye.Main);
    }
    public void Open()
    {
        transform.Find("Menu").gameObject.SetActive(true);
    }
    public void Close()
    {
        transform.Find("Menu").gameObject.SetActive(false);
    }
}
