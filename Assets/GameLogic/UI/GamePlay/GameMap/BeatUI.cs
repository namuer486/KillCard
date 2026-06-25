using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeatUI : MonoBehaviour,IPoolable,IPointerClickHandler
{

    public Beat beat {  get; internal set; }
    public void OnBack()
    {
        beat = null;
    }

    public void OnGet()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(beat != null&&beat.isOpen)
        {
            FrameworkCore.Event.OnTriggerEven("LoadCurrentEnemy", beat.id);
            FrameworkCore.Senes.LoadTempScene(TempScene.Attack, () => { FrameworkCore.Event.OnTriggerEven("ChangeTempState", GameTempType.player); });
        }
    }
}
