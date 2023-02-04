using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public enum EMode
    {
        PlayMode,
        EditMode
    }
    public EMode gameMode;
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
            gameMode = EMode.PlayMode;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if(gameMode == EMode.PlayMode)
            Input.OnUpdate();
    }
}
