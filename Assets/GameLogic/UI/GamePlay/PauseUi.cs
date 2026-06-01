using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour,BasePanel
{
    public Button BackMenu;
    public Button BackGame;
    private void OnEnable()
    {
        BackMenu.onClick.AddListener(ChangeMenu);
        BackGame.onClick.AddListener(PauseOver);
    }
    private void OnDisable()
    {
        BackMenu.onClick.RemoveAllListeners();
        BackGame.onClick.RemoveAllListeners();
    }
    private void ChangeMenu()
    {
        //TODO：删除暂停状态，改用全局bool控制流程,弹窗显示暂停界面
        FrameworkCore.UI.ClearPopup();
        FrameworkCore.Senes.ChangeScene(GameType.menu);
    }
    private void PauseOver()
    {
        FrameworkCore.Event.OnTriggerEven("GamePuaseOver");
        FrameworkCore.UI.Pop(UILaye.Popup);
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
