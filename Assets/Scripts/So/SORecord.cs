using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecordData", menuName = "ScriptableObjects/RecordData", order = 1)]
public class SORecord : ScriptableObject, ISerializationCallbackReceiver
{
    public string RecordName;
    public List<int> inputData;
    public List<float> inputTimeData;

    public void OnAfterDeserialize()
    {
        RecordName = null;
        inputData.Clear();
        inputTimeData.Clear();
    }

    public void OnBeforeSerialize()
    {
    }
}
