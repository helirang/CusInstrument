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
    /// CurrentAudioClips의 데이터를 변경하는 함수
    /// </summary>
    /// <param name="isCustom">true면 커스텀 / false면 기본 악기로 변경된다</param>
    /// <returns>입력 값대로 변경되면 true / 실패하면 false 반환</returns>
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
