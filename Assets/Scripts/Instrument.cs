using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(SoundTracks),typeof(SoundLoader),typeof(AudioExtension))]
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

    #region 커스텀 컴포넌트 && 클래스
    [Header("Custom")]
    [SerializeField] private SoundTracks soundTracks;
    [SerializeField] private SoundLoader soundLoader;
    [SerializeField] private AudioExtension audioExtension;
    #endregion

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private MusicRecorder musicRecorder;
    private bool isRecod = false;
    private bool isReplay = false;
    private bool isPlayMode = true;
    public bool IsPercussionInstrument { get; set; }
    private IEnumerator replayCoroutine = null;
    public UnityEvent RecordEvenet;

    private void Start()
    {
        IsPercussionInstrument = !true;

        musicRecorder = new MusicRecorder();

        soundTracks = soundTracks == null ? 
            this.gameObject.GetComponent<SoundTracks>() : soundTracks;
        soundLoader = soundLoader == null ?
            this.gameObject.GetComponent<SoundLoader>() : soundLoader;
        audioExtension = audioExtension == null ?
            this.gameObject.GetComponent<AudioExtension>() : audioExtension;

        AudioSourceGenerations(audioSources,soundTracks.CurrentAudioClips,audioMixerGroup);
        audioExtension.FadeOutSetting(soundTracks.CurrentAudioClips.Count);
        InstrumentMode = EInstrumentMode.PianoMode;
    }

    private void Update()
    {
        if(isPlayMode) 
            OnKeyboard();
    }

    #region Audio 생성 및 음원 설정 관련 함수
    public void InstrumentModeChange(int modeNum)
    {
        InstrumentMode = (EInstrumentMode)modeNum;
    }
    void ModeChange(EInstrumentMode modeNum)
    {
        RecodrStop();
        ReplayStop(ref isReplay, replayCoroutine);
        switch (modeNum)
        {
            case EInstrumentMode.PianoMode:
                SoundChange(false);
                isPlayMode = true;
                break;
            case EInstrumentMode.CustomMode:
                SoundChange(true);
                isPlayMode = true;
                break;
            case EInstrumentMode.EditMode:
                isPlayMode = false;
                break;
        }
    }

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

    /// <summary>
    /// true면 커스텀 음원으로 false면 피아노 음원으로 변경
    /// </summary>
    /// <param name="isCustom"></param>
    void SoundChange(bool isCustom)
    {
        if (soundTracks.ChangeSoundTracks(isCustom))
            SoundSetting(soundTracks.CurrentAudioClips);
    }
    public void CustomSoundLoad()
    {
        StartCoroutine(soundLoader.AudioLoad(CustomSoundSave));
    }
    public void CustomSoundSave()
    {
        soundTracks.CustomAudioClips = soundLoader.AudioClips;
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

    #region 연주 관련 함수 (실시간 재생, 악보 재생)
    void Play(int audioNum)
    {
        if(!IsPercussionInstrument && audioNum < 0)
        {
            Debug.Log(audioNum);
            int convertNum = -(audioNum + 1);
            audioExtension.FadeOutStart(convertNum,audioSources[convertNum]);
            if (isRecod)musicRecorder.Record(audioNum);
        }
        else if(audioNum >= 0)
        {
            audioExtension.FadeOutStop(audioNum, audioSources[audioNum]);
            audioSources[audioNum].Play();
            if (isRecod) musicRecorder.Record(audioNum);
        }
    }

    public void ReplayOnOff()
    {
        Debug.Log(isReplay);
        if (isReplay) 
        { 
            ReplayStop(ref isReplay, replayCoroutine);
            isPlayMode = true;
        }
        else
        {
            isPlayMode = false;
            replayCoroutine =
                Replay(musicRecorder.GetInputData(), musicRecorder.GetInputTimeData());
            StartCoroutine(replayCoroutine);
            if (isRecod) RecodrStop();
        };
    }

    IEnumerator Replay(List<int> inputData, List<float> inputTimeData)
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

    void ReplayStop(ref bool isWork, IEnumerator enumerator)
    {
        if (enumerator != null && isWork)
        {
            StopCoroutine(enumerator);
            isWork = false;
        }
    }
    #endregion

    #region 악보 기록,로드,저장 관련 함수
    public void RecordOnOff()
    {
        if (isReplay || isRecod) RecodrStop();
        else Record();
    }

    void Record()
    {
        Debug.Log("RecordStart");
        if (instrumentMode == EInstrumentMode.PianoMode || InstrumentMode == EInstrumentMode.CustomMode)
        {
            musicRecorder.Initialize();
            isRecod = true;
        }
    }

    void RecodrStop()
    {
        RecordEvenet.Invoke();
        isRecod = false;
    }

    public void LoadSheet(string data)
    {
        Debug.Log(data);
    }

    public void SaveSheet()
    {
        musicRecorder.SaveData();
    }
    #endregion
}
