using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUi : MonoBehaviour, BasePanel, IPointerClickHandler//½ÇÉ«Ñ¡Ôñ¿¨Æ¬
{
    public PlayerConfig config { get; internal set; }
    private void Start()
    {
        TextMeshProUGUI text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Sprite sprite =GetComponent<Sprite>();
        text.text = config.name;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        FrameworkCore.UI.ClearPopup();
        FrameworkCore.Senes.ChangeScene(GameType.gameplay);
        if (config == null)
        {
            Debug.LogError("¿¨Æ¬UIÄÚIDÎ´ÉèÖÃ");
            return;
        }
        GameCore.Player.LoadPlayer(config.id - 1);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(true);
    }
}
