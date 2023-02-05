using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundTracks),typeof(SoundLoader))]
public class Instrument : MonoBehaviour
{
    enum EInstrumentMode 
    { 
        PianoMode,
        CustomMode
    }
    [SerializeField] EInstrumentMode instrumentMode;
    public string sfx;

    private MusicRecorder musicRecorder;
    private List<AudioSource> audioSources = new List<AudioSource>();

    [Header("RuntimeField")]

    [Header("EditField")]
    [SerializeField] private SoundTracks soundTracks;

    private void Start()
    {
        musicRecorder = new MusicRecorder();
        Managers.Input.KeyAction += OnKeyboard;
        AudioSourceGenerations(soundTracks.CurrentAudioClips);
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyboard;
    }

    void AudioSourceGenerations(List<AudioClip> audioClips)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClips[i];
            audioSources.Add(audioSource);
        }
    }

    void SoundSetting(List<AudioClip> audioClips)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            audioSources[i].Stop();
            audioSources[i].clip = audioClips[i];
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A)) Play(0);
        else if (Input.GetKeyDown(KeyCode.S)) Play(1);
        else if (Input.GetKeyDown(KeyCode.D)) Play(2);
        else if (Input.GetKeyDown(KeyCode.F)) Play(3);
        else if (Input.GetKeyDown(KeyCode.H)) Play(4);
        else if (Input.GetKeyDown(KeyCode.J)) Play(5);
        else if (Input.GetKeyDown(KeyCode.K)) Play(6);
        else if (Input.GetKeyDown(KeyCode.L)) Play(7);
        else if (Input.GetKeyDown(KeyCode.W)) Play(8);
        else if (Input.GetKeyDown(KeyCode.E)) Play(9);
        else if (Input.GetKeyDown(KeyCode.T)) Play(10);
        else if (Input.GetKeyDown(KeyCode.U)) Play(11);
        else if (Input.GetKeyDown(KeyCode.I)) Play(12);
        else if (Input.GetKeyDown(KeyCode.P)) Play(13);
    }

    void Play(int audioNum)
    {
        audioSources[audioNum].Play();
        musicRecorder.Record(audioNum);
    }
}
