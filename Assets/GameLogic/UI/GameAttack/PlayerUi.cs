using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    public Player player { get; internal set; }
    public TextMeshProUGUI Name {  get; internal set; }
    public TextMeshProUGUI HP {  get; internal set; }
    // Start is called before the first frame update
    void Awake()
    {
        Name = transform.Find("name").GetComponent<TextMeshProUGUI>();
        HP = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        FrameworkCore.Event.Add(this, "UpDateUI", UpDateUI);

        UpDateUI();
    }
    public void UpDateUI()
    {
        player=GameCore.Player.currentplayer;
        if (Name != null)
        {
            Name.text = player.Config.name;
        }
        if (HP != null)
        {
            HP.text = "HP:" + player.HP.ToString();
        }

    }
}
