using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUi : MonoBehaviour
{
    public Enemy enemy {  get; internal set; }
    //TODO:π÷ ﬁ≥ı ºªØ
    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("π÷ ﬁŒ¥≈‰÷√");
            return;
        }
        TextMeshProUGUI name = transform.Find("name").GetComponent<TextMeshProUGUI>();
        if (name != null)
        {
            name.text = enemy.Config.name;
        }
        TextMeshProUGUI hp = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        if (hp != null)
        {
            hp.text = "HP:" + enemy.Config.HP.ToString();
        }
    }
}
