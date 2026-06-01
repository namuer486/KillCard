using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    public Player player { get; internal set; }
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Íæ¼ÒÎ´Ñ¡Ôñ");
            return;
        }
        TextMeshProUGUI name = transform.Find("name").GetComponent<TextMeshProUGUI>();
        if (name != null)
        {
            name.text = player.config.name;
        }
        TextMeshProUGUI hp= transform.Find("HP").GetComponent<TextMeshProUGUI>();
        if (hp != null)
        {
            hp.text = "HP:" + player.config.HP.ToString();
        }
    }
}
