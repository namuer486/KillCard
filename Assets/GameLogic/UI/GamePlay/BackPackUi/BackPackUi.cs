using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackUi : MonoBehaviour,BasePanel//TODO：完成背包格子Ui和背包数据的交互
{
    public Transform BagContent;//背包区域
    public Button BagExit;
    public Pool<BagGridUi> pool {  get; private set; }
    public List<BagGridUi> currentgrid { get; private set; }
    public BagGridUi grid { get; private set; }
    public BagDate Date { get; private set; }
    public int count { get; private set; } = 20;
    private void Awake()
    {
        grid = FrameworkCore.Resourse.ResourcesLoad<BagGridUi>(ABConfig.Normal, "BagGridPre");
        if(grid == null)
        {
            Debug.LogError("格子预制体未找到");
            return;
        }
        pool = new Pool<BagGridUi>(grid, count, BagContent);
        currentgrid=new List<BagGridUi>();
        FrameworkCore.Event.Add<BagDate>(this, "UpDateBackUI", UpDateBackUI);
        UpDateBackUI(GameCore.BackPack.bag);
        
    }
    private void UpDateBackUI(BagDate date)
    {
        if (date.handCards.Length>count)
        {
            Debug.Log("扩容对象池");
            return;
        }
        Date = date;
        for (int i=0; i< currentgrid.Count; i++)
        {
            BagGridUi gridUi = currentgrid[i];
            if(gridUi != null)
            {
                gridUi.UpDateCardUI(Date.handCards[i].config);
            }
        }
    }
    public void Open()
    {
        BagExit.onClick.AddListener(Exit);
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        for (int i = 0; i < count; i++)
        {
            BagGridUi gridUi = pool.Get();
            gridUi.UpDateCardUI(Date.handCards[i]?.config);
            currentgrid.Add(gridUi);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < currentgrid.Count; i++)
        {
            BagGridUi gridUi = currentgrid[i];
            pool.Back(gridUi);
        }
        currentgrid.Clear();
        BagExit.onClick.RemoveAllListeners();
    }
    private void Exit()
    {
        FrameworkCore.UI.Pop(UILaye.Popup);
    }
}
