using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour,BasePanel
{
    public Button StartUI;
    public Button Internet;
    public Transform BackGrand;
    // Start is called before the first frame update
    void Start()
    {
        StartUI.onClick.AddListener(ChangeToStart);
        Internet.onClick.AddListener(InterNetStart);
    }
    private void ChangeToStart()
    {
        FrameworkCore.Senes.ChangeScene(GameType.charactor);
    }
    private void InterNetStart()
    {
        FrameworkCore.Senes.ChangeScene(GameType.charactor);
    }
    private void OnDestroy()
    {
        StartUI.onClick.RemoveAllListeners();
        Internet.onClick.RemoveAllListeners();
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
