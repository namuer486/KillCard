using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameworkCore
{
    private static FrameworkCore instance = null;
    public static FrameworkCore Instance
    {
        get
        {
            if(instance!=null)
                return instance;
            instance = new FrameworkCore();
            return instance;
        }
    }
    public static UIManager UI { get; internal set; }
    public static EventSystem Event { get; internal set; }
    public static ScenesManager Senes { get; internal set; }
    public static ResourseManager Resourse { get; internal set; }
    public FrameworkCore()
    {
        UI = UIManager.Instance;
        Event = EventSystem.Instance;
        Senes = ScenesManager.Instance;
        Resourse = ResourseManager.Instance;
    }
   

}
