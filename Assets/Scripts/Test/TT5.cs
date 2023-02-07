using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT5 : MonoBehaviour
{
    PlayerInputActions inputActions;

    public void InSet()
    {
        inputActions = new PlayerInputActions();
        Debug.Log(inputActions.Instrument.C4.bindings.Count);
    }
}
