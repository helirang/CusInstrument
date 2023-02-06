using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracks : MonoBehaviour
{
    [Header("EditField")]
    [SerializeField] InsSoundTracks instrumentSoundTracks;

    public List<AudioClip> CustomAudioClips;
    public List<AudioClip> CurrentAudioClips { get; private set; }
    bool isCustom = false;

    private void Awake()
    {
        CurrentAudioClips = instrumentSoundTracks.soundTracks;
    }

    /// <summary>
    /// true면 커스텀으로 변경, false면 기본 악기로 변경
    /// </summary>
    /// <param name="isCustom"></param>
    public bool ChangeSoundTracks(bool isCustom)
    {
        bool isChanged = this.isCustom != isCustom;
        if(isChanged)
        {
            if (!isCustom && CustomAudioClips == null) 
                isChanged = false;
            else
            {
                CurrentAudioClips = isCustom ? 
                    CustomAudioClips : instrumentSoundTracks.soundTracks;
                this.isCustom = isCustom;
            }
        }
        return isChanged;
    }
}
