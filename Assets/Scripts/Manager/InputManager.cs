using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public delegate void keyEventHandler();
    public event keyEventHandler KeyAction;

    public void OnUpdate()
    {
        if (Input.anyKey == false) return;
        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
}
