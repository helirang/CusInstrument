using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TT : MonoBehaviour
{
    private void Start()
    {
        Managers.Input.KeyAction -= Test;
        Managers.Input.KeyAction += Test;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= Test;
    }
    void Test()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("d");
        }
    }
}
