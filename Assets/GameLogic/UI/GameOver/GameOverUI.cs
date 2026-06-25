using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button ReBack;
    private void Awake()
    {
        if(ReBack != null)
        {
            ReBack.onClick.AddListener(ReBackGameMenu);
        }
    }
    private void OnDestroy()
    {
        ReBack.onClick.RemoveAllListeners();
    }
    private void ReBackGameMenu()
    {
        FrameworkCore.Senes.ChangeScene(GameType.menu);
    }
}
