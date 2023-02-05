using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracks : MonoBehaviour
{
    [Header("EditField")]
    [SerializeField] InsSoundTracks instrumentSoundTracks;
    public List<AudioClip> CustomAudioClips { get; set; }
    public List<AudioClip> CurrentAudioClips { get; private set; }

    private void Awake()
    {
        CurrentAudioClips = instrumentSoundTracks.soundTracks;
    }

    /// <summary>
    /// true면 커스텀으로 변경, false면 기본 악기로 변경
    /// </summary>
    /// <param name="isCustom"></param>
    public void ChangeSoundTracks(bool isCustom)
    {
        CurrentAudioClips = isCustom ? CustomAudioClips : instrumentSoundTracks.soundTracks;
    }
}
