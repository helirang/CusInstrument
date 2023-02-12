using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentData", menuName = "ScriptableObjects/InstrumentSoundTracksScriptableObject", order = 1)]
public class InsSoundTracks : ScriptableObject
{
    public string instrumentName;
    public List<AudioClip> soundTracks;
}
