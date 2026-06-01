using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum UILaye
{
    Background,
    Main,
    Popup,
    Guide//引导文本

}
public interface BasePanel
{
    public void Open();
    public void Close();
}
public class UIManager : MonoBehaviour
{
    //private static UIManager instance = null;
    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (instance != null)
    //            return instance;
    //        instance = new UIManager();
    //        return instance;
    //    }
    //}
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindFirstObjectByType<UIManager>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject("UIManager");
                instance = gameObject.AddComponent<UIManager>();
            }
            return instance;
        }
    }
    public Dictionary<UILaye, Stack<BasePanel>> pairs { get; private set; }//弹窗线字典
    public GameObject TopCanvas { get; private set; }
    public List<GameObject> BackgroundList {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        CreateTopCanvas();
        GameObject Pause = Resources.Load<GameObject>("Prefab/PauseUi");
        BackgroundList.Add(Pause);
        DontDestroyOnLoad(gameObject);
    }
    public UIManager()
    {
        pairs = new Dictionary<UILaye, Stack<BasePanel>>();
        BackgroundList = new List<GameObject>();
        pairs.Add(UILaye.Background, new Stack<BasePanel>());
        pairs.Add(UILaye.Main, new Stack<BasePanel>());
        pairs.Add(UILaye.Popup, new Stack<BasePanel>());
        pairs.Add(UILaye.Guide, new Stack<BasePanel>());
    }
    public void Push(UILaye laye,BasePanel panel)
    {
        if (panel == null)
            return;
        Stack<BasePanel> panels = pairs[laye];
        if (panels.Contains(panel))
            return;
        panel.Open();
        panels.Push(panel);
    }
    public void Pop(UILaye laye)
    {
        Stack<BasePanel> panels = pairs[laye];
        if (panels.Count <= 0)
        {
            Debug.LogError(laye + "空了");
            return;
        }
        BasePanel panel = panels.Pop();
        panel.Close();
    }
    public bool ClearPopup()
    {
        Stack<BasePanel> panels = pairs[UILaye.Popup];
        if (panels.Count <= 0)
            return true;
        foreach (BasePanel panel in panels)
        {
            panel.Close();
        }
        panels.Clear();
        return true;
    }
    private void CreateTopCanvas()
    {
        //创建一个跨场景的Ui层
        GameObject SystemCavans = new GameObject("SystemCavans");
        Canvas canvas = SystemCavans.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.sortingOrder = 99;
        SystemCavans.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(SystemCavans);
        TopCanvas = SystemCavans;
    }
    public void LoadBackGroundUi()
    {
        for(int i=0;i< BackgroundList.Count; i++)
        {
            GameObject pause = Instantiate(BackgroundList[i]);
            pause.gameObject.SetActive(false);
            pause.transform.SetParent(TopCanvas.transform, false);
        }
        
    }
    public void UnLoadBackGroundUi(string name)
    {
        Transform pause = TopCanvas.transform.Find(name);
        Destroy(pause);
    }
}
