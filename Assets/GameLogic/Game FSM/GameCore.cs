using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
    init,
    menu,
    charactor,
    gameplay,
    over
}
public class GameCore : MonoBehaviour
{
    public static GameCore Instance = null;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
        DontDestroyOnLoad(gameObject);
    }
    private StateManager<GameType> GameManager = null;
    public bool Is_Pause {  get; internal set; }
    public HandCardUi HandCardPrefab { get; private set; }

    public static InputManager Input = null;
    public static PlayerManager Player = null;
    public static EnemyManager Enemy = null;
    public static CharacterFactory CharacterFactory = null;
    public static CardsFactory CardsFactory = null;
    public static HandCardManager HandCard = null;
    public static BackPackManager BackPack = null;
    public static Pool<HandCardUi> HandCardPool = null;

    private void Update()
    {
        Input.OnUpdate();
        GameManager.currentState.OnUpDate();
    }
    private void Init()
    {
        //状态机初始化(需要剥离为Game Manager)
        Is_Pause = false;
        GameManager = new StateManager<GameType>();
        GameManager.AddState(GameType.init, new GameInit());
        GameManager.AddState(GameType.menu, new GameMenu());
        GameManager.AddState(GameType.charactor, new GameCharactor());
        GameManager.AddState(GameType.gameplay, new GamePlay());
        GameManager.AddState(GameType.over, new GameOver());
        FrameworkCore.Event.Add<GameType>(this, "ChangeMainState", ChangeState);
        GameManager.ChangeState(GameType.init);
        //内部功能初始化
        Input = InputManager.Instance;
        Player = PlayerManager.Instance;
        Enemy = EnemyManager.Instance;
        CharacterFactory = CharacterFactory.Instance;
        CardsFactory = CardsFactory.Instance;
        HandCard = HandCardManager.Instance;
        BackPack = BackPackManager.Instance;
        //对象池初始化(需要剥离为Card Pool)
        HandCardPrefab = FrameworkCore.Resourse.ResourcesLoad<HandCardUi>("Prefab/HandCard");
        if (HandCardPrefab == null)
        {
            Debug.LogError("卡牌预制体不存在");
        }
        HandCardPool = new Pool<HandCardUi>(HandCardPrefab, 10);

        FrameworkCore.Senes.ChangeScene(GameType.menu);
    }
    public void ChangeState(GameType type)
    {
        if (GameManager == null)
            return;
        GameManager.ChangeState(type);
    }
}
