using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SoundTracks),typeof(SoundLoader),typeof(AudioExtension))]
public class Instrument : MonoBehaviour
{
    public enum ESoundMode 
    { 
        PianoMode,
        CustomMode
    }
    public enum EInstrumentMode
    {
        PlayMode,
        RePlayMode,
        EditMode
    }
    EInstrumentMode instrumentMode;

    private PlayerInputActions inputActions;
    #region Ŀ���� ������Ʈ && Ŭ����
    [Header("Custom")]
    [SerializeField] private SoundTracks soundTracks;
    [SerializeField] private SoundLoader soundLoader;
    [SerializeField] private AudioExtension audioExtension;
    #endregion

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private MusicRecorder musicRecorder;
    private bool isRecod = false;
    public bool IsPercussionInstrument { get; set; }
    private IEnumerator replayCoroutine = null;
    public UnityEvent RecordEvenet;
    public UnityEvent<string> SaveSheetEvent;

    private void Awake()
    {
        KeyboardSetting();
    }
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
        CustomSoundLoad();
        SoundModeChange(ESoundMode.PianoMode);
        InstrumentModeChange(EInstrumentMode.PlayMode);
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    #region ��Ʈ�ѷ� ������ �Լ�
    void InstrumentModeChange(EInstrumentMode instrumentMode)
    {
        switch (instrumentMode)
        {
            case EInstrumentMode.PlayMode:
                ReplayStop(replayCoroutine);
                inputActions.Enable();
                break;
            case EInstrumentMode.RePlayMode:
                inputActions.Disable();
                if (isRecod) RecodStop();
                replayCoroutine =
                    Replay(musicRecorder.GetInputData(), musicRecorder.GetInputTimeData());
                StartCoroutine(replayCoroutine);
                break;
            case EInstrumentMode.EditMode:
                inputActions.Disable();
                if (isRecod) RecodStop();
                ReplayStop(replayCoroutine);
                break;
        }
        this.instrumentMode = instrumentMode;
    }
    public void ModeChange(int modeNum)
    {
        InstrumentModeChange((EInstrumentMode)modeNum);
    }
    #endregion

    #region Audio ���� �� ���� ���� ���� �Լ�
    void SoundModeChange(ESoundMode modeNum)
    {
        switch (modeNum)
        {
            case ESoundMode.PianoMode:
                SoundChange(false);
                break;
            case ESoundMode.CustomMode:
                SoundChange(true);
                break;
        }
    }
    public void InstrumentSoundModeChange(int modeNum)
    {
        SoundModeChange((ESoundMode)modeNum);
    }

    /// <summary>
    /// ����� �ҽ� ������Ʈ ���� �Լ�
    /// </summary>
    /// <param name="audioSources"> ������ ������ҽ� ������Ʈ���� ������ ���� �ʵ�</param>
    /// <param name="audioClips"> �ش� ���� ������ŭ ������ҽ� ������Ʈ ����</param>
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
    void AudioSoundSetting(List<AudioClip> audioClips)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            audioSources[i].Stop();
            audioSources[i].clip = audioClips[i];
        }
    }

    /// <summary>
    /// true�� Ŀ���� �������� false�� �ǾƳ� �������� ����
    /// </summary>
    /// <param name="isCustom"></param>
    void SoundChange(bool isCustom)
    {
        if (soundTracks.ChangeSoundTracks(isCustom))
            AudioSoundSetting(soundTracks.CurrentAudioClips);
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

    #region Ű�Է� ���� �Լ�
    void KeyboardSetting()
    {
        inputActions = new PlayerInputActions();
        inputActions.Instrument.Enable();
        inputActions.Instrument.C4.started += (InputAction.CallbackContext context) => Play(0); ;
        inputActions.Instrument.D4.started += (InputAction.CallbackContext context) => Play(1); ;
        inputActions.Instrument.E4.started += (InputAction.CallbackContext context) => Play(2); ;
        inputActions.Instrument.F4.started += (InputAction.CallbackContext context) => Play(3); ;
        inputActions.Instrument.G4.started += (InputAction.CallbackContext context) => Play(4); ;
        inputActions.Instrument.A4.started += (InputAction.CallbackContext context) => Play(5); ;
        inputActions.Instrument.B4.started += (InputAction.CallbackContext context) => Play(6); ;
        inputActions.Instrument.C5.started += (InputAction.CallbackContext context) => Play(7); ;
        inputActions.Instrument.C4Sharp.started += (InputAction.CallbackContext context) => Play(8); ;
        inputActions.Instrument.D4Sharp.started += (InputAction.CallbackContext context) => Play(9); ;
        inputActions.Instrument.F4Sharp.started += (InputAction.CallbackContext context) => Play(10); ;
        inputActions.Instrument.G4Sharp.started += (InputAction.CallbackContext context) => Play(11); ;
        inputActions.Instrument.A4Sharp.started += (InputAction.CallbackContext context) => Play(12); ;
        inputActions.Instrument.C5Sharp.started += (InputAction.CallbackContext context) => Play(13); ;

        inputActions.Instrument.C4.canceled += (InputAction.CallbackContext context) => Play(-1); ;
        inputActions.Instrument.D4.canceled += (InputAction.CallbackContext context) => Play(-2); ;
        inputActions.Instrument.E4.canceled += (InputAction.CallbackContext context) => Play(-3); ;
        inputActions.Instrument.F4.canceled += (InputAction.CallbackContext context) => Play(-4); ;
        inputActions.Instrument.G4.canceled += (InputAction.CallbackContext context) => Play(-5); ;
        inputActions.Instrument.A4.canceled += (InputAction.CallbackContext context) => Play(-6); ;
        inputActions.Instrument.B4.canceled += (InputAction.CallbackContext context) => Play(-7); ;
        inputActions.Instrument.C5.canceled += (InputAction.CallbackContext context) => Play(-8); ;
        inputActions.Instrument.C4Sharp.canceled += (InputAction.CallbackContext context) => Play(-9); ;
        inputActions.Instrument.D4Sharp.canceled += (InputAction.CallbackContext context) => Play(-10); ;
        inputActions.Instrument.F4Sharp.canceled += (InputAction.CallbackContext context) => Play(-11); ;
        inputActions.Instrument.G4Sharp.canceled += (InputAction.CallbackContext context) => Play(-12); ;
        inputActions.Instrument.A4Sharp.canceled += (InputAction.CallbackContext context) => Play(-13); ;
        inputActions.Instrument.C5Sharp.canceled += (InputAction.CallbackContext context) => Play(-14); ;
    }
    #endregion

    #region ���� ���� �Լ� (�ǽð� ���, �Ǻ� ���)
    void Play(int audioNum)
    {
        if (!IsPercussionInstrument && audioNum < 0)
        {
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
        //@todo �ӽ��ڵ� �Ǻ� �Է� ui�� �����ϸ� ������ ��.
        if (instrumentMode == EInstrumentMode.EditMode) return;

        if (instrumentMode == EInstrumentMode.RePlayMode) 
            InstrumentModeChange(EInstrumentMode.PlayMode);
        else InstrumentModeChange(EInstrumentMode.RePlayMode);
    }

    IEnumerator Replay(List<int> inputData, List<float> inputTimeData)
    {
        for (int i = 0; i < inputData.Count; i++)
        {
            yield return new WaitForSeconds(inputTimeData[i]);
            Play(inputData[i]);
        }
        InstrumentModeChange(EInstrumentMode.PlayMode);
    }

    void ReplayStop(IEnumerator enumerator)
    {
        //Debug.Log(enumerator);
        if (enumerator != null) StopCoroutine(enumerator);
    }
    #endregion

    #region �Ǻ� ���,�ε�,���� ���� �Լ�
    public void RecordOnOff()
    {
        //Debug.Log("RecordStop");
        if (instrumentMode == EInstrumentMode.PlayMode && !isRecod)
            Record();
        else RecodStop();
    }

    void Record()
    {
        //Debug.Log("RecordStart");
        if (instrumentMode == EInstrumentMode.PlayMode)
        {
            musicRecorder.Initialize();
            isRecod = true;
        }
    }

    void RecodStop()
    {
        RecordEvenet.Invoke();
        isRecod = false;
    }

    public void LoadSheet(string data)
    {
        bool result = musicRecorder.SetData(data);
        //Debug.Log(result);
    }

    public void SaveSheet(bool isBackUp)
    {
        musicRecorder.SaveData(isBackUp, CallBackSave);
    }

    void CallBackSave(string data)
    {
        SaveSheetEvent.Invoke(data);
    }
    #endregion
}
