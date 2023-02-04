using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public float initalValue;
    [System.NonSerialized]
    public float runtimeValue;
    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        runtimeValue = initalValue;
    }
}
