using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public FloatVariable hp;
    public FloatVariable floatVariable;
    public FloatVariable floatVariable2;

    // Start is called before the first frame update
    void Start()
    {
        floatVariable = ScriptableObject.CreateInstance<FloatVariable>();
        floatVariable2 = ScriptableObject.CreateInstance<FloatVariable>();
        floatVariable.initalValue = hp.initalValue;
        hp.runtimeValue = 5;
        floatVariable2.initalValue = hp.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hp.runtimeValue);
        Debug.Log(floatVariable.initalValue);
        Debug.Log(floatVariable.runtimeValue);
        Debug.Log(floatVariable2.runtimeValue);
    }
}
