using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public string instrumentName;
    public string sfx;

    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioClip> currentAudioClips;
    [SerializeField]private List<AudioClip> pianoAudioClips;
    [SerializeField]private List<AudioClip> customAudioClips;
    [SerializeField]InsSoundTracks pianoSoundTracks;

    [SerializeField]IntVariable instrumentMode;


    private void Start()
    {
        Managers.Input.KeyAction -= Play;
        Managers.Input.KeyAction += Play;
        pianoAudioClips = pianoSoundTracks.soundTracks;
        currentAudioClips = pianoAudioClips;
        SoundSetting();
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= Play;
    }

    void SoundSetting()
    {
        for (int i = 0; i < currentAudioClips.Count; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = currentAudioClips[i];
            audioSources.Add(audioSource);
        }
    }
    void Play()
    {
        if (Input.GetKeyDown(KeyCode.A)) audioSources[0].Play();
        else if (Input.GetKeyDown(KeyCode.S)) audioSources[1].Play();
        else if (Input.GetKeyDown(KeyCode.D)) audioSources[2].Play();
        else if (Input.GetKeyDown(KeyCode.F)) audioSources[3].Play();
        else if (Input.GetKeyDown(KeyCode.H)) audioSources[4].Play();
        else if (Input.GetKeyDown(KeyCode.J)) audioSources[5].Play();
        else if (Input.GetKeyDown(KeyCode.K)) audioSources[6].Play();
        else if (Input.GetKeyDown(KeyCode.L)) audioSources[7].Play();
        else if (Input.GetKeyDown(KeyCode.W)) audioSources[8].Play();
        else if (Input.GetKeyDown(KeyCode.E)) audioSources[9].Play();
        else if (Input.GetKeyDown(KeyCode.T)) audioSources[10].Play();
        else if (Input.GetKeyDown(KeyCode.U)) audioSources[11].Play();
        else if (Input.GetKeyDown(KeyCode.I)) audioSources[12].Play();
        else if (Input.GetKeyDown(KeyCode.P)) audioSources[13].Play();
    }
}
