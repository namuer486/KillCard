using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUi : MonoBehaviour
{
    public Enemy enemy {  get; internal set; }
    public TextMeshProUGUI Name {  get; internal set; }
    public TextMeshProUGUI Hp {  get; internal set; }

    //TODO:π÷ ﬁ≥ı ºªØ
    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("π÷ ﬁŒ¥≈‰÷√");
            return;
        }
        Name = transform.Find("name").GetComponent<TextMeshProUGUI>();
        Hp = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        UpDateUI();
        FrameworkCore.Event.Add(this, "UpDateEnemyUI", UpDateUI);
    }
    private void UpDateUI()
    {
        Name.text = enemy.Config.name;
        Hp.text = "HP:" + enemy.HP.ToString();
    }
}
