using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUi : MonoBehaviour//地图Ui
{
    public Transform PauseUi {  get; private set; }
    public Button StartAttack;//Test

    private void OnEnable()
    {
        FrameworkCore.Event.Add(this, "GamePuase", PauseBegin);
        StartAttack.onClick.AddListener(AttackBegin);//Test
    }
    private void Start()
    {
        PauseUi = FrameworkCore.UI.TopCanvas.transform.Find("PauseUi(Clone)");
        if (PauseUi == null)
        {
            Debug.LogError("跨场景Canvas里没有这个组件");
        }
    }
    private void OnDestroy()
    {
        FrameworkCore.Event.RemoveAll(this);
        StartAttack.onClick.RemoveAllListeners();//Test
    }
    private void PauseBegin()
    {
        FrameworkCore.UI.Push(UILaye.Popup, PauseUi.GetComponent<PauseUi>());
    }
    private void AttackBegin()
    {
        FrameworkCore.Event.OnTriggerEven("LoadCurrentEnemy", 0);//DOTO：按钮内部需要内置关卡id
        FrameworkCore.Senes.LoadTempScene(GameTempType.attack);
    }
    public void Open()
    {
        transform.Find("Menu").gameObject.SetActive(true);
    }
    public void Close()
    {
        transform.Find("Menu").gameObject.SetActive(true);
    }
}
