using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(SoundTracks),typeof(SoundLoader))]
public class Instrument : MonoBehaviour
{
    public enum EInstrumentMode 
    { 
        PianoMode,
        CustomMode,
        EditMode
    }
    EInstrumentMode instrumentMode;
    public EInstrumentMode InstrumentMode 
    {
        get { return instrumentMode; }
        set
        {
            instrumentMode = value;
            Debug.Log(instrumentMode);
            ModeChange(value);
        }
    }

    #region 오디오 관련 변수
    [Header("Audio")]
    [SerializeField] private SoundTracks soundTracks;
    [SerializeField] private SoundLoader soundLoader;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private List<AudioSource> audioSources = new List<AudioSource>();
    #endregion

    private MusicRecorder musicRecorder;
    private bool isRecod = false;
    private bool isReplay = false;
    private bool isPlayMode = true;
    public bool IsPercussionInstrument { get; set; }
    private IEnumerator replayCoroutine = null;
    private List<IEnumerator> fadeCoroutines = new List<IEnumerator>();
    private List<bool> isFadeOuts = new List<bool>();
    public UnityEvent RecordEvenet;

    private void Start()
    {
        musicRecorder = new MusicRecorder();

        soundTracks = soundTracks == null ? 
            this.gameObject.GetComponent<SoundTracks>() : soundTracks;
        soundLoader = soundLoader == null ?
            this.gameObject.GetComponent<SoundLoader>() : soundLoader;
        IsPercussionInstrument = !true;

        AudioSourceGenerations(audioSources,soundTracks.CurrentAudioClips,audioMixerGroup);
        FadeOutSetting(soundTracks.CurrentAudioClips.Count);
        InstrumentMode = EInstrumentMode.PianoMode;
    }
    private void OnDisable()
    {
        //DisconnectKeyboard();
    }

    private void Update()
    {
        if(isPlayMode) 
            OnKeyboard();
    }
    public void InstrumentModeChange(int modeNum)
    {
        InstrumentMode = (EInstrumentMode)modeNum;
    }
    void ModeChange(EInstrumentMode modeNum)
    {
        RecodrEnd();
        ReplayEnd(ref isReplay,replayCoroutine);
        switch (modeNum)
        {
            case EInstrumentMode.PianoMode:
                if(soundTracks.ChangeSoundTracks(false))
                    SoundSetting(soundTracks.CurrentAudioClips);
                isPlayMode = true;
                break;
            case EInstrumentMode.CustomMode:
                if (soundTracks.ChangeSoundTracks(true))
                    SoundSetting(soundTracks.CurrentAudioClips);
                isPlayMode = true;
                break;
            case EInstrumentMode.EditMode:
                isPlayMode = false;
                break;
        }
    }

    #region Audio 및 SoundTracks 관련 함수
    /// <summary>
    /// 오디오 소스 컴포넌트 생성 함수
    /// </summary>
    /// <param name="audioSources"> 생성된 오디오소스 컴포넌트들의 참조를 담은 필드</param>
    /// <param name="audioClips"> 해당 변수 개수만큼 오디오소스 컴포넌트 생성</param>
    void AudioSourceGenerations(List<AudioSource> audioSources , List<AudioClip> audioClips ,AudioMixerGroup audioMixerGroup)   
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClips[i];
            audioSources.Add(audioSource);
            audioSource.outputAudioMixerGroup = audioMixerGroup;
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
    public void CustomSoundLoad()
    {
        StartCoroutine(soundLoader.AudioLoad2(CustomSoundSave));
    }
    void CustomSoundSave()
    {
        soundTracks.CustomAudioClips = soundLoader.AudioClips;
    }

    void FadeOutSetting(int max)
    {
        for (int i=0; i < max; i++)
        {
            isFadeOuts.Add(false);
            fadeCoroutines.Add(null);
        }
    }
    void FadeOutStart(int audioNum)
    {
        float duration = 1f;
        float targetVolume = 0f;
        isFadeOuts[audioNum] = true;
        fadeCoroutines[audioNum] = FadeOut(audioNum, duration, targetVolume, audioSources[audioNum]);
        StartCoroutine(fadeCoroutines[audioNum]);
    }
    void FadeOutStop(int audioNum,AudioSource audioSource,IEnumerator enumerator)
    {
        if (isFadeOuts[audioNum]) 
        {
            StopCoroutine(enumerator);
            audioSource.Stop();
            audioSource.volume = 1f;
            isFadeOuts[audioNum] = false;
        }
    }
    IEnumerator FadeOut(int audioNum, float duration,float targetVolume, AudioSource audioSource)
    {
        isFadeOuts[audioNum] = true;
        float currentTime = 0;
        float start = audioSource.volume;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 1f;
        isFadeOuts[audioNum] = false;
    }
    #endregion

    #region 키입력 관련 함수
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

        if (Input.GetKeyUp(KeyCode.A)) Play(-1);
        else if (Input.GetKeyUp(KeyCode.S)) Play(-2);
        else if (Input.GetKeyUp(KeyCode.D)) Play(-3);
        else if (Input.GetKeyUp(KeyCode.F)) Play(-4);
        else if (Input.GetKeyUp(KeyCode.H)) Play(-5);
        else if (Input.GetKeyUp(KeyCode.J)) Play(-6);
        else if (Input.GetKeyUp(KeyCode.K)) Play(-7);
        else if (Input.GetKeyUp(KeyCode.L)) Play(-8);
        else if (Input.GetKeyUp(KeyCode.W)) Play(-9);
        else if (Input.GetKeyUp(KeyCode.E)) Play(-10);
        else if (Input.GetKeyUp(KeyCode.T)) Play(-11);
        else if (Input.GetKeyUp(KeyCode.U)) Play(-12);
        else if (Input.GetKeyUp(KeyCode.I)) Play(-13);
        else if (Input.GetKeyUp(KeyCode.P)) Play(-14);
    }
    #endregion

    #region 연주 관련 함수 ( 연주, 악보 등 )
    void Play(int audioNum)
    {
        if(!IsPercussionInstrument && audioNum < 0)
        {
            Debug.Log(audioNum);
            int convertNum = -(audioNum + 1);
            FadeOutStart(convertNum);
            if(isRecod)musicRecorder.Record(audioNum);
        }
        else if(audioNum >= 0)
        {
            FadeOutStop(audioNum, audioSources[audioNum], fadeCoroutines[audioNum]);
            audioSources[audioNum].Play();
            if (isRecod) musicRecorder.Record(audioNum);
        }
    }

    public void LoadSheet(string data)
    {
        Debug.Log(data);
    }

    public void Record()
    {
        if (isReplay||isRecod) RecodrEnd();
        else RecordStart();
    }

    void RecordStart()
    {
        Debug.Log("RecordStart");
        if(instrumentMode == EInstrumentMode.PianoMode || InstrumentMode == EInstrumentMode.CustomMode)
        {
            musicRecorder.Initialize();
            isRecod = true;
        }
    }
    void RecodrEnd()
    {
        RecordEvenet.Invoke();
        isRecod = false;
    }

    public void Replay()
    {
        Debug.Log(isReplay);
        if (isReplay) 
        { 
            ReplayEnd(ref isReplay, replayCoroutine);
            isPlayMode = true;
        }
        else
        {
            isPlayMode = false;
            replayCoroutine =
                ReplayStart(musicRecorder.GetInputData(), musicRecorder.GetInputTimeData());
            StartCoroutine(replayCoroutine);
            if (isRecod) RecodrEnd();
        };
    }

    IEnumerator ReplayStart(List<int> inputData, List<float> inputTimeData)
    {
        isReplay = true;
        Debug.Log(this.isReplay);
        for (int i = 0; i < inputData.Count; i++)
        {
            yield return new WaitForSeconds(inputTimeData[i]);
            Play(inputData[i]);
        }
        isReplay = false;
        isPlayMode = true;
    }

    void ReplayEnd(ref bool isWork, IEnumerator enumerator)
    {
        if (enumerator != null && isWork)
        {
            StopCoroutine(enumerator);
            Debug.Log(isReplay);
            isWork = false;
            Debug.Log(isReplay);
        }
    }
    #endregion

    #region 세이브 관련 함수
    public void SaveSheet()
    {
        musicRecorder.SaveData();
    }
    #endregion
}
