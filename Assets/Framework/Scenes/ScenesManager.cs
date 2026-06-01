using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager instance = null;
    public static ScenesManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindFirstObjectByType<ScenesManager>();
            if(instance == null)
            {
                GameObject gameObject = new GameObject("ScenesManager");
                instance = gameObject.AddComponent<ScenesManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(GameType type)
    {
        switch (type)
        {
            case GameType.menu:
                StartCoroutine(LoadSceneAsync("MenuScene" , type));
                break;
            case GameType.charactor:
                StartCoroutine(LoadSceneAsync("CharotorChoose", type));
                break;
            case GameType.gameplay:
                StartCoroutine(LoadSceneAsync("GameMapPlay", type));
                break;
            default:
                break;
        }
        
    }
    private IEnumerator LoadSceneAsync(string scene, GameType type)
    {
        // 异步加载场景
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);

        // 等待加载完成
        yield return asyncOperation;
        FrameworkCore.Event.OnTriggerEven("ChangeMainState", type);
        Debug.Log("场景切换完成！");
    }
    public void LoadTempScene(GameTempType type)//副场景异步加载
    {
        switch (type)
        {
            case GameTempType.attack:
                StartCoroutine(LoadTempSceneAsync("AttackScene", type));
                break;
            default:
                break;

        }
    }
    private IEnumerator LoadTempSceneAsync(string scene, GameTempType type)
    {
        // 异步加载场景
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        // 等待加载完成
        yield return asyncOperation;
        FrameworkCore.Event.OnTriggerEven("ChangeTempState", type);
        Debug.Log("副场景切换完成！");
    }
    public void UnLoadTempScene(GameTempType type)
    {
        switch (type)
        {
            case GameTempType.attack:
                SceneManager.UnloadSceneAsync("AttackScene");
                break;
            default:
                break;

        }
        FrameworkCore.Event.OnTriggerEven("ChangeTempState", GameTempType.normal);
    }
}
