using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }
    public static InputManager Input { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Input = new InputManager();
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        Input.OnUpdate();
    }
}
