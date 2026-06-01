using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUi : MonoBehaviour
{
    public Button BackMap;
    public int CardNum = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        BackMap.onClick.AddListener(ReBackMap);
        GameCore.CharacterFactory.PlayerCreate(transform);
        GameCore.CharacterFactory.EnemyCreate(transform);
    }
    private void OnDisable()
    {
        BackMap.onClick.RemoveAllListeners();
    }

    private void ReBackMap()
    {
        FrameworkCore.Senes.UnLoadTempScene(GameTempType.attack);
    }
}
