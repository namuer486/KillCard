using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUi : MonoBehaviour//TODO:需要把卡牌UI对象池移动到此处，完成敌人AI
{
    public Button BackMap;
    public Button Over;//结束当前回合
    public Button BagEnter;
    public BackPackUi BagUI;
    public Transform PlayerBuff;
    public Transform EnemyBuff;
    public BuffUI BuffPre {  get; private set; }
    public HandCardUi CardPre {  get; private set; }
    public Pool<BuffUI> BuffUIPool {  get; private set; }
    public Pool<HandCardUi> HandCardUIPool {  get; private set; }
    public List<HandCardUi> cardUis {  get; private set; }=new List<HandCardUi>();
    public List<BuffUI> PlayerbuffUis {  get; private set; }=new List<BuffUI>();
    public List<BuffUI> EnemybuffUis {  get; private set; }=new List<BuffUI>();
    public Vector2 PosCard { get; private set; } = new Vector2(-250, -70);
    public float PosSize = 100;
    // Start is called before the first frame update
    void OnEnable()
    {
        BackMap.onClick.AddListener(ReBackMap);
        Over.onClick.AddListener(OverNow);
        BagEnter.onClick.AddListener(BagChange);
        GameCore.CharacterFactory.PlayerCreate(transform);
        GameCore.CharacterFactory.EnemyCreate(transform);
        FrameworkCore.Event.Add<List<HandCard>>(this, "UpDateHandCardUI", UpDateHandCardUI);
        FrameworkCore.Event.Add<List<Buff>>(this, "UpDetePlayerBuffUI", UpDetePlayerBuffUI);
        FrameworkCore.Event.Add<List<Buff>>(this, "UpDeteEnemyBuffUI", UpDeteEnemyBuffUI);
        FrameworkCore.Event.Add(this, "ClearAllHandCard", ClearHandCard);
        FrameworkCore.Event.Add(this, "ClearAllBuffUI", ClearBuffUI);
        //FrameworkCore.Event.Add(this, "AttackWin", AttackWin);
        BuffPre = FrameworkCore.Resourse.ResourcesLoad<BuffUI>(ABConfig.Normal, "BuffPre");
        CardPre = FrameworkCore.Resourse.ResourcesLoad<HandCardUi>(ABConfig.Card, "HandCard");
        BuffUIPool = new Pool<BuffUI>(BuffPre, 10, transform);
        HandCardUIPool = new Pool<HandCardUi>(CardPre, 20, transform);
    }
    private void OnDestroy()
    {
        BackMap.onClick.RemoveAllListeners();
        Over.onClick.RemoveAllListeners();
        BagEnter.onClick.RemoveAllListeners();
        FrameworkCore.Event.RemoveAll(this);
        //ClearHandCard();
        //ClearPlayerBuffUI();
        //ClearEnemyBuffUI();
        BuffPre = null;
        CardPre = null;
        HandCardUIPool.Clear();
        BuffUIPool.Clear();
    }
    public void BagChange()
    {
        FrameworkCore.UI.Push(UILaye.Popup, BagUI);
    }
    private void ReBackMap()
    {
        GameCore.Buff.Clear();
        FrameworkCore.Senes.UnLoadTempScene(TempScene.Attack);
    }
    private void OverNow()//结束当前回合
    {
        FrameworkCore.Event.OnTriggerEven("ChangeTempState",GameTempType.enemy);
    }
    private void UpDateHandCardUI(List<HandCard> date)
    {
        Vector2 pos = PosCard;
        ClearHandCard();
        foreach (var card in date)
        {
            if(card != null)
            {
                HandCardUi ui = HandCardUIPool.Get();
                ui.GetComponent<RectTransform>().localPosition= pos;
                ui.UpdateUI(card);
                cardUis.Add(ui);
            }
            pos += Vector2.right * PosSize;
        }
    }
    private void ClearHandCard()
    {
        foreach (var handCard in cardUis)
        {
            if (handCard != null)
            {
                HandCardUIPool.Back(handCard);
            }
        }
        cardUis.Clear();
    }
    private void UpDetePlayerBuffUI(List<Buff> buffs)
    {
        ClearPlayerBuffUI();
        foreach (var card in buffs)
        {
            BuffUI ui=BuffUIPool.Get();
            ui.UpDateUI(card);
            ui.transform.SetParent(PlayerBuff, false);
            PlayerbuffUis.Add(ui);
        }
    }
    private void ClearPlayerBuffUI()
    {
        foreach (var buff in PlayerbuffUis)
        {
            if(buff != null)
                BuffUIPool.Back(buff);
        }
        PlayerbuffUis.Clear();
    }
    private void UpDeteEnemyBuffUI(List<Buff> buffs)
    {
        ClearEnemyBuffUI();
        foreach (var card in buffs)
        {
            BuffUI ui=BuffUIPool.Get();
            ui.UpDateUI(card);
            ui.transform.SetParent(PlayerBuff, false);
            EnemybuffUis.Add(ui);
        }
    }
    private void ClearEnemyBuffUI()
    {
        foreach (var buff in EnemybuffUis)
        {
            if(buff != null)
                BuffUIPool.Back(buff);
        }
        EnemybuffUis.Clear();
    }
    private void ClearBuffUI()
    {
        ClearEnemyBuffUI();
        ClearPlayerBuffUI();
    }
}
