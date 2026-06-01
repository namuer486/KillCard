using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static InputManager instance = null;
    public static InputManager Instance
    {
        get
        {
            if(instance != null)
                return instance;
            instance = new InputManager();
            return instance;
        }
    }
    public void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab)&&!GameCore.Instance.Is_Pause)
        {
            FrameworkCore.Event.OnTriggerEven("GamePuase");
        }
    }
}
