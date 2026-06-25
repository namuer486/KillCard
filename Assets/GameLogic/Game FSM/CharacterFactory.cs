using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    private static CharacterFactory instance = null;
    public static CharacterFactory Instance
    {
        get
        {
            if(instance != null)
                return instance;
            instance = FindFirstObjectByType<CharacterFactory>();
            if(instance == null)
            {
                GameObject gameObject = new GameObject("CharacterFactory");
                instance = gameObject.AddComponent<CharacterFactory>();
            }
            return instance;
        }
    }
    public Transform PlayerAni {  get; private set; }
    public Transform EnemyAni {  get; private set; }
    public Vector2 PosPlayer { get; private set; } = new Vector2(-400, 0);
    public Vector2 PosEnemy { get; private set; } = new Vector2(400, 0);
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        //PlayerAni = FrameworkCore.Resourse.ResourcesLoad<Transform>(ABConfig.Character, "Player");
        //EnemyAni = FrameworkCore.Resourse.ResourcesLoad<Transform>(ABConfig.Character, "Enemy");
    }
    public void PlayerCreate(Transform Parent)
    {
        //玩家初始化
        PlayerAni = FrameworkCore.Resourse.ResourcesLoad<GameObject>(ABConfig.Character, "Player").transform;
        if (PlayerAni == null)
        {
            Debug.LogError("角色预制体未配置");
            return;
        }
        Transform player = Instantiate<Transform>(PlayerAni);
        player.SetParent(Parent);
        player.GetComponent<RectTransform>().localPosition = PosPlayer;
        PlayerUi playerUi = player.GetComponent<PlayerUi>();
        playerUi.player = GameCore.Player.currentplayer;
    }
    public void EnemyCreate(Transform Parent)
    {
        //敌人初始化
        EnemyAni = FrameworkCore.Resourse.ResourcesLoad<GameObject>(ABConfig.Character, "Enemy").transform;
        if (EnemyAni == null)
        {
            Debug.LogError("敌人预制体未配置");
            return;
        }
        Transform enemy = Instantiate<Transform>(EnemyAni);
        enemy.SetParent(Parent);
        enemy.GetComponent<RectTransform>().localPosition = PosEnemy;
        EnemyUi enemyUi = enemy.GetComponent<EnemyUi>();
        enemyUi.enemy = GameCore.Enemy.currentenemy;
    }
}
