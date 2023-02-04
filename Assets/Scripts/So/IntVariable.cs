using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentData", menuName = "ScriptableObjects/IntVariable", order = 1)]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public string purpose;
    public int initalValue;
    [System.NonSerialized]
    public int runtimeValue;
    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        runtimeValue = initalValue;
    }
}
